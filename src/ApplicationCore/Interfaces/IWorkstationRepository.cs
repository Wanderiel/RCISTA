using Domain.Models.Workstations;

namespace ApplicationCore.Interfaces;

public interface IWorkstationRepository
{
    void Insert(Workstation workstation);
    Task<List<Workstation>> GetAll(CancellationToken cancellationToken = default);
    Task<Workstation?> GetById(WorkstationId workstationId, CancellationToken cancellationToken = default);
}
