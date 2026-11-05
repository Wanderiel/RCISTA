using ApplicationCore.Dtos.WorkStations;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Domain.Models.Workstations;

namespace ApplicationCore.Services;

public class WorkstationService
{
    private readonly IWorkstationRepository _repository;
    private readonly INonPaperMediaRepository _npmRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkstationService(IWorkstationRepository repository, INonPaperMediaRepository npmRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
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

    public async Task<bool> AddDisk(int workstationId, int diskId)
    {
        Workstation workstation = await GetWorkstation(workstationId);
        NonPaperMedia disk = await GetDisk(diskId);

        if (await _npmRepository.IsAvailableAsync(disk.Id) == false)
            throw new InvalidOperationException($"Диск (s/n: {disk.SerialNumber}) уже установлен в другую рабочую станцию.");

        workstation.AddDisk(disk);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveDisk(int workstationId, int diskId)
    {
        Workstation workstation = await GetWorkstation(workstationId);
        NonPaperMedia disk = await GetDisk(diskId);

        if (workstation.HasDisk(disk.Id))
            workstation.RemoveDisk(disk);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private async Task<Workstation> GetWorkstation(int workstationId)
    {
        WorkstationId id = new WorkstationId(workstationId);
        Workstation? workstation = await _repository.GetByIdAsync(id)
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
