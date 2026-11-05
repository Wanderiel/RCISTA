namespace ApplicationCore.Dtos.WsHistory;

public class TaskRequest
{
    public string Number { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? ActionDate { get; set; }
    public required int WorkstationId { get; set; }
    public required List<int> AddedDisks { get; set; } = [];
    public required List<int> RemovedDisks { get; set; } = [];
    public string? Remarks { get; set; } = string.Empty;

}
