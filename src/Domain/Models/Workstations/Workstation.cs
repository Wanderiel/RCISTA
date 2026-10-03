using Domain.Interfaces;
using Domain.Models.NPMs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Workstations;

public class Workstation : IChangedAt
{
    private Workstation() { }

    public Workstation(string inventoryNumber) =>
        InventoryNumber = inventoryNumber;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public WorkstationId Id { get; private set; }
    [Required, StringLength(50)]
    public string InventoryNumber { get; private set; }
    [Required]
    public bool IsDecommissioned { get; private set; } = false;
    //public UserId AutorId { get; private set; }
    //public UserId ChangedId { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<NonPaperMedia> Disks { get; private set; } = [];

    public void AddDisk(NonPaperMedia disk) =>
        Disks.Add(disk);

    public void RemoveDisk(NonPaperMedia disk) =>
        Disks.Remove(disk);

    public void WriteOff() =>
        IsDecommissioned = true;

    public void ToRegister() =>
        IsDecommissioned = false;
}
