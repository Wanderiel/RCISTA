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

    public async Task Create(CreatedWorkstationDto workstationDto, CancellationToken cancellationToken = default)
    {
        Workstation workstation = new Workstation(workstationDto.InventoryNumber.ToUpper());
        _repository.Insert(workstation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<WorkstationResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _repository.GetAllAsync(cancellationToken)).Select(w => w.ToDto()).ToList();

    public async Task<WorkstationResponse?> GetAsync(int id, CancellationToken cancellationToken = default) =>
        (await GetWorkstation(id, cancellationToken))?.ToDto();

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Workstation? workstation = await GetWorkstation(id, cancellationToken);
        workstation.WriteOff();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task CreateTask(TaskRequest taskDto, CancellationToken cancellationToken = default)
    {
        Workstation workstation = await GetWorkstation(taskDto.WorkstationId, cancellationToken);
        StringBuilder remarksBuilder = new StringBuilder(taskDto.Remarks);
        List<NonPaperMedia> addedDisks = await FindDisksAsync(taskDto.AddedDiskIds, cancellationToken);
        List<NonPaperMedia> removedDisks = await FindDisksAsync(taskDto.RemovedDiskIds, cancellationToken);

        User user = new User("test", FullName.Create("test", "test", "test"), "12345678"); //TODO заглушка

        AddDiskInfoToRemarks("Установлено в РС: ", remarksBuilder, addedDisks);
        AddDiskInfoToRemarks("Изьято из РС: ", remarksBuilder, removedDisks);
        WorkstationHistoryItem historyItem = new WorkstationHistoryItem(
            workstation, taskDto.ActionDate, taskDto.Description, taskDto.Number, user, remarksBuilder.ToString());

        await AddDisks(workstation, addedDisks, cancellationToken);
        workstation.RemoveDisks(removedDisks);
        _historyRepository.Insert(historyItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<List<NonPaperMedia>> FindDisksAsync(List<int> intIds, CancellationToken cancellationToken = default)
    {
        List<NonPaperMedia> result = [];

        if (intIds.Count == 0)
            return result;

        List<NpmId> npmIds = intIds.Select(intId => new NpmId(intId)).ToList();
        result = await _npmRepository.GetByIdsAsync(npmIds, cancellationToken);

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

    private void AddDiskInfoToRemarks(string comment, StringBuilder remarksBuilder, List<NonPaperMedia> disks)
    {
        if (disks.Count > 0)
        {
            remarksBuilder.AppendLine(comment);

            foreach (NonPaperMedia disk in disks)
                remarksBuilder.AppendLine(disk.ToString());
        }
    }

    private async Task AddDisks(Workstation workstation, List<NonPaperMedia> disks, CancellationToken cancellationToken = default)
    {
        foreach (NonPaperMedia disk in disks)
        {
            if (await _npmRepository.IsAvailableAsync(disk.Id, cancellationToken) == false)
                throw new InvalidOperationException($"Диск (s/n: {disk.SerialNumber}) уже установлен в другую рабочую станцию.");
        }

        workstation.AddDisks(disks);
    }

    private async Task<Workstation> GetWorkstation(int workstationId, CancellationToken cancellationToken = default)
    {
        WorkstationId id = new WorkstationId(workstationId);
        Workstation? workstation = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new MissingWorksationException(workstationId);

        return workstation;
    }
}
