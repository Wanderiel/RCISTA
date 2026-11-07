using Domain.Models.Workstations;

namespace ApplicationCore.Interfaces;

public interface IWorkstationRepository
{
    void Insert(Workstation workstation);
    Task<List<Workstation>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Workstation?> GetByIdAsync(WorkstationId workstationId, CancellationToken cancellationToken = default);
}
