using ApplicationCore.Dtos.WorkStations;
using Domain.Models.Workstations;

namespace ApplicationCore.Extentions;

public static class WorkstationExstentions
{
    public static WorkstationResponse ToDto(this Workstation workstation)
    {
        return new WorkstationResponse()
        {
            InventoryNumber = workstation.InventoryNumber,
            IsDecommissioned = workstation.IsDecommissioned,
            Disks = workstation.Disks.Select(d => d.ToDto()).ToList(),
        };
    }
}
