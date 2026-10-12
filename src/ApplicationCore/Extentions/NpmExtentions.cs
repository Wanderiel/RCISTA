using ApplicationCore.Dtos.NPMs;
using Domain.Models.NPMs;

namespace ApplicationCore.Extentions;

public static class NpmExtentions
{
    public static NpmResponse ToDto(this NonPaperMedia nonPaperMedia) =>
        new NpmResponse()
        {
            Type = nonPaperMedia.Type.ToString(),
            Manufacturer = nonPaperMedia.Manufacturer.Name,
            Model = nonPaperMedia.Model.Name,
            SerialNumber = nonPaperMedia.SerialNumber,
            Capacity = nonPaperMedia.Capacity
        };
}
