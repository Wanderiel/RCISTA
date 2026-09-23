namespace ApplicationCore.Dtos.NPMs;

public class CreatedNPMdto
{
    public required string Type { get; set; }
    public required string Manufacturer { get; set; }
    public required string Model { get; set; }
    public required string SerialNumber { get; set; }
    public required int Capacity { get; set; }
}
