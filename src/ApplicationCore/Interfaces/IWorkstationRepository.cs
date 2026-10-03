using Domain.Models.Workstations;

namespace ApplicationCore.Interfaces;

public interface IWorkstationRepository
{
    void Insert(Workstation workstation);
    Task<List<Workstation>> GetAllAsync();
    Task<Workstation?> GetByIdAsync(WorkstationId workstationId);
    void Delete(Workstation workstation);
}
