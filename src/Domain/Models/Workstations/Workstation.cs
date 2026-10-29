using Domain.Interfaces;
using Domain.Models.History;
using Domain.Models.NPMs;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Workstations;

public class Workstation : IChangedAt
{
    private Workstation() { }

    public Workstation(string inventoryNumber) =>
        InventoryNumber = inventoryNumber;

    public WorkstationId Id { get; set; }
    [Required, StringLength(50)]
    public string InventoryNumber { get; private set; }
    [Required]
    public bool IsDecommissioned { get; private set; } = false;
    //public UserId AutorId { get; private set; }
    //public UserId ChangedId { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<WorkstationStateRecord> WorkstationStateRecords { get; private set; } = [];

    public void AddDisk(NonPaperMedia disk) =>
        throw new Exception();

    public void RemoveDisk(NonPaperMedia disk) =>
        throw new Exception();

    public void WriteOff() =>
        IsDecommissioned = true;

    public void ToRegister() =>
        IsDecommissioned = false;
}
