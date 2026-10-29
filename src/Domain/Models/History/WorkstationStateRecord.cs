using Domain.Models.NPMs;
using Domain.Models.Workstations;

namespace Domain.Models.History;

public class WorkstationStateRecord
{
    private WorkstationStateRecord() { }

    public WorkstationStateRecord(Workstation workstation, string inventoryNumber, bool isDecommissioned, List<NonPaperMedia> disks)
    {
        Workstation = workstation;
        InventoryNumber = inventoryNumber;
        IsDecommissioned = isDecommissioned;
        Disks = disks;
        StartDate = DateTime.Now;
    }

    public WsrId Id { get; }
    public Workstation Workstation { get; }
    public string InventoryNumber { get; }
    public bool IsDecommissioned { get; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<NonPaperMedia>? Disks { get; }
}
