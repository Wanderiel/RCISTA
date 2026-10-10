using Domain.Models.NPMs;

namespace ApplicationCore.Dtos.NPMs;

public class CreatedNPMdto
{
    public required NpmType Type { get; set; }
    public required string Manufacturer { get; set; }
    public required Model Model { get; set; }
    public required string SerialNumber { get; set; }
    public required int Capacity { get; set; }
}
