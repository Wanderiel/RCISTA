using ApplicationCore.Dtos.NPMs;

namespace ApplicationCore.Dtos.WorkStations;

public class WorkstationResponse
{
    public required string InventoryNumber { get; set; }
    public required bool IsDecommissioned { get; set; }
    public required List<NpmResponse> Disks { get; set; }
}
