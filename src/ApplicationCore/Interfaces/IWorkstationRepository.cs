using Domain.Models.Workstations;

namespace ApplicationCore.Interfaces;

public interface IWorkstationRepository
{
    Task<List<Workstation>> GetAllAsync();
    Task<Workstation?> GetByIdAsync(WorkstationId workstationId);
    void Delete(Workstation workstation);
}
