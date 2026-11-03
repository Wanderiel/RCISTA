using ApplicationCore.Dtos.WsHistory;
using Domain.Models.History;

namespace ApplicationCore.Extentions;

public static class WsHistoryExtentions
{
    public static WsHistoryResponse ToDto(this WorkstationHistoryItem item)
    {
        return new WsHistoryResponse()
        {
            WorkstationName = item.Workstation.InventoryNumber,
            ActionDate = DateOnly.FromDateTime(DateTime.Now),
            WorkDescription = item.WorkDescription,
            TaskNumber = item.TaskNumber,
            UserName = item.User.FullName.ToString(),
            DiskNames = item.Disks?.Select(d => d.ToString())?.ToList(),
            Remarks = item.Remarks,
        };
    }
}
