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
    private readonly NonPaperMediaService _diskService;
    private readonly IUnitOfWork _unitOfWork;

    public WorkstationService(
        IWorkstationRepository repository,
        IWsHistoryRepository historyRepository,
        NonPaperMediaService diskService,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _historyRepository = historyRepository;
        _diskService = diskService;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreatedWorkstationDto workstationDto, CancellationToken cancellationToken = default)
    {
        Workstation workstation = new Workstation(workstationDto.InventoryNumber.ToUpper());
        _repository.Insert(workstation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<WorkstationResponse>> GetAll(CancellationToken cancellationToken = default) =>
        (await _repository.GetAll(cancellationToken)).Select(w => w.ToDto()).ToList();

    public async Task<WorkstationResponse?> Get(int id, CancellationToken cancellationToken = default) =>
        (await GetWorkstation(id, cancellationToken))?.ToDto();

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
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
        List<NonPaperMedia> addedDisks = await _diskService.FindDisks(taskDto.AddedDiskIds, cancellationToken);
        List<NonPaperMedia> removedDisks = await _diskService.FindDisks(taskDto.RemovedDiskIds, cancellationToken);

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
            if (await _diskService.IsAvailable(disk.Id, cancellationToken) == false)
                throw new NonPaperMediaAlreadyAssignedException(disk.SerialNumber);
        }

        workstation.AddDisks(disks);
    }

    private async Task<Workstation> GetWorkstation(int workstationId, CancellationToken cancellationToken = default)
    {
        WorkstationId id = new WorkstationId(workstationId);
        Workstation workstation = await _repository.GetById(id, cancellationToken)
            ?? throw new MissingWorksationException(workstationId);

        return workstation;
    }
}
