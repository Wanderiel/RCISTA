using Domain.Models.NPMs;
using Domain.Models.Users;
using Domain.Models.Workstations;

namespace Domain.Models.History;

public class WorkstationHistoryItem
{
    private WorkstationHistoryItem() { }

    public WorkstationHistoryItem(
        WorkstationId workstationId,
        DateOnly? actionDate,
        string workDescription,
        string taksNumber,
        UserId userId,
        List<NonPaperMedia>? disks,
        string? remarks)
    {
        WorkstationId = workstationId;
        WorkDescription = workDescription;
        TaskNumber = taksNumber;
        UserId = userId;
        Disks = disks?.ToList();
        Remarks = remarks;

        if (actionDate == null)
            ActionDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public WsHId Id { get; }
    public WorkstationId WorkstationId { get; }
    public DateOnly ActionDate { get; }
    public string WorkDescription { get; private set; } = string.Empty;
    public string TaskNumber { get; private set; } = string.Empty;
    public UserId UserId { get; private set; }
    public List<NonPaperMedia>? Disks { get; }
    public string? Remarks { get; private set; }
}
