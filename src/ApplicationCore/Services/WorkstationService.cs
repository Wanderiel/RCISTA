using ApplicationCore.Dtos.WorkStations;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;
using Domain.Models.Workstations;

namespace ApplicationCore.Services;

public class WorkstationService
{
    private readonly IWorkstationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkstationService(IWorkstationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
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
}
