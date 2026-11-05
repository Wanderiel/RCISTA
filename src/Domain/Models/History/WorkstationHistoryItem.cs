using Domain.Models.NPMs;
using Domain.Models.Users;
using Domain.Models.Workstations;

namespace Domain.Models.History;

public class WorkstationHistoryItem
{
    private WorkstationHistoryItem() { }

    public WorkstationHistoryItem(
        Workstation workstation,
        DateOnly? actionDate,
        string workDescription,
        string taksNumber,
        User user,
        string? remarks)
    {
        Workstation = workstation;
        WorkDescription = workDescription;
        TaskNumber = taksNumber;
        User = user;
        Disks = workstation?.Disks?.ToList();
        Remarks = remarks;

        //TODO добавить проверку на завтрашний день
        if (actionDate == null)
            ActionDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public WsHId Id { get; }
    public Workstation Workstation { get; }
    public DateOnly ActionDate { get; }
    public string WorkDescription { get; private set; } = string.Empty;
    public string TaskNumber { get; private set; } = string.Empty;
    public User User { get; private set; }
    public List<NonPaperMedia>? Disks { get; }
    public string? Remarks { get; private set; }
}
