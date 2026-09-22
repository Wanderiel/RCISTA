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

    public async Task<List<Workstation>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Workstation?> GetAsync(int id)
    {
        WorkstationId workstationId = new WorkstationId(id);

        return await _repository.GetByIdAsync(workstationId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        WorkstationId workstationId = new WorkstationId(id);
        Workstation? workstation = await _repository.GetByIdAsync(workstationId);

        if (workstation == null)
            return false;

        _repository.Delete(workstation);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
