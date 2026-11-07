using ApplicationCore.Dtos.WorkStations;
using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Exceptions;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;
using Domain.Models.History;
using Domain.Models.NPMs;
using Domain.Models.Users;
using Domain.Models.Workstations;
using System.Text;

namespace ApplicationCore.Services;

public class WorkstationService
{
    private readonly IWorkstationRepository _repository;
    private readonly IWsHistoryRepository _historyRepository;
    private readonly INonPaperMediaRepository _npmRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkstationService(
        IWorkstationRepository repository,
        IWsHistoryRepository historyRepository,
        INonPaperMediaRepository npmRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _historyRepository = historyRepository;
        _npmRepository = npmRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreatedWorkstationDto workstationDto)
    {
        Workstation workstation = new Workstation(workstationDto.InventoryNumber.ToUpper());
        _repository.Insert(workstation);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<WorkstationResponse>> GetAllAsync() =>
        (await _repository.GetAllAsync()).Select(w => w.ToDto()).ToList();

    public async Task<WorkstationResponse?> GetAsync(int id)
    {
        WorkstationId workstationId = new WorkstationId(id);

        return (await _repository.GetByIdAsync(workstationId))?.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        WorkstationId workstationId = new WorkstationId(id);
        Workstation? workstation = await _repository.GetByIdAsync(workstationId);

        if (workstation == null)
            return false;

        workstation.WriteOff();
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task CreateTask(TaskRequest taskDto, CancellationToken cancellationToken)
    {
        Workstation workstation = await GetWorkstation(taskDto.WorkstationId, cancellationToken);
        StringBuilder remarksBuilder = new StringBuilder(taskDto.Remarks);
        List<NonPaperMedia> addedDisks = [];
        List<NonPaperMedia> removedDisks = [];
        User user = new User("test", FullName.Create("test", "test", "test"), "12345678"); //TODO заглушка

        if (taskDto.AddedDisks.Count > 0)
        {
            remarksBuilder.AppendLine("Установлено в РС: ");
            addedDisks = await FindDisksAsync(taskDto.AddedDisks, cancellationToken);

            foreach (NonPaperMedia disk in addedDisks)
                remarksBuilder.AppendLine(disk.ToString());
        }

        if (taskDto.RemovedDisks.Count > 0)
        {
            remarksBuilder.AppendLine("Изьято из РС: ");
            removedDisks = await FindDisksAsync(taskDto.RemovedDisks, cancellationToken);

            foreach (NonPaperMedia disk in removedDisks)
                remarksBuilder.AppendLine(disk.ToString());
        }

        WorkstationHistoryItem historyItem = new WorkstationHistoryItem(
            workstation, taskDto.ActionDate, taskDto.Description, taskDto.Number, user, remarksBuilder.ToString());

        await AddDisks(workstation, addedDisks, cancellationToken);
        workstation.RemoveDisks(removedDisks);
        _historyRepository.Insert(historyItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<List<NonPaperMedia>> FindDisksAsync(List<int> intIds, CancellationToken cancellationToken = default)
    {
        List<NpmId> npmIds = intIds.Select(intId => new NpmId(intId)).ToList();
        List<NonPaperMedia> result = await _npmRepository.GetByIdsAsync(npmIds, cancellationToken);

        if (result.Count < intIds.Count)
        {
            HashSet<int> missingIds = intIds.ToHashSet()
                .Except(
                    result.Select(d => d.Id.Value)
                    .ToHashSet())
                .ToHashSet();

            throw new MissingNonPaperMediaException(missingIds);
        }

        return result;
    }

    private async Task AddDisks(Workstation workstation, List<NonPaperMedia> disks, CancellationToken cancellationToken)
    {
        foreach (NonPaperMedia disk in disks)
        {
            if (await _npmRepository.IsAvailableAsync(disk.Id, cancellationToken) == false)
                throw new InvalidOperationException($"Диск (s/n: {disk.SerialNumber}) уже установлен в другую рабочую станцию.");
        }

        workstation.AddDisks(disks);
    }

    private async Task<Workstation> GetWorkstation(int workstationId, CancellationToken cancellationToken)
    {
        WorkstationId id = new WorkstationId(workstationId);
        Workstation? workstation = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException($"Рабочая станция c ID {workstationId} не найдена.");

        return workstation;
    }

    private async Task<NonPaperMedia> GetDisk(int diskId)
    {
        NpmId npmId = new NpmId(diskId);
        NonPaperMedia? disk = await _npmRepository.GetByIdAsync(npmId)
            ?? throw new InvalidOperationException($"Диск с ID {diskId} не найден.");

        return disk;
    }
}
