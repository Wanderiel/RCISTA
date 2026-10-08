using Domain.Models.NPMs;

namespace ApplicationCore.Dtos.NPMs;

public class UpdatedNPMDto
{
    public NpmType Type { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public int Capacity { get; set; }
    public NpmStatus Status { get; set; }
}
