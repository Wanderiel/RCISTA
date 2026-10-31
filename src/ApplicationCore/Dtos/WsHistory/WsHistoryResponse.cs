namespace ApplicationCore.Dtos.WsHistory;

public class WsHistoryResponse
{
    public required string WorkstationName { get; set; }
    public DateOnly ActionDate { get; set; }
    public string WorkDescription { get; set; } = string.Empty;
    public string TaskNumber { get; set; } = string.Empty;
    public required string UserName { get; set; }
    public required List<string> DiskNames { get; set; }
    public string? Remarks { get; set; } = string.Empty;
}
