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
        WorkstationId id = new WorkstationId(workstationId);
        Workstation? workstation = await _repository.GetByIdAsync(id);

        if (workstation == null)
            return false;

        NpmId npmId = new NpmId(diskId);
        NonPaperMedia? disk = workstation.WorkstationStateRecords.FirstOrDefault(d => d.Id == npmId);

        if (disk == null)
            disk = await _npmRepository.GetByIdAsync(npmId);
        else
            return false;

        if (disk == null)
            return false;

        workstation.WorkstationStateRecords.Add(disk);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveDisk(int workstationId, int diskId)
    {
        WorkstationId id = new WorkstationId(workstationId);
        Workstation? workstation = await _repository.GetByIdAsync(id);

        if (workstation == null)
            return false;

        NpmId npmId = new NpmId(diskId);
        NonPaperMedia? disk = workstation.WorkstationStateRecords.FirstOrDefault(d => d.Id == npmId);

        if (disk == null)
            return false;

        workstation.WorkstationStateRecords.Remove(disk);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
