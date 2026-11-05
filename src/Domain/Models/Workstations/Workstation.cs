using Domain.Interfaces;
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
    public List<NonPaperMedia> Disks { get; private set; } = [];

    public void AddDisk(NonPaperMedia disk)
    {
        if (HasDisk(disk.Id))
            throw new InvalidOperationException(
                $"Диск (s/n: {disk.SerialNumber}) уже присуствует в рабочей станции {InventoryNumber}.)");

        Disks.Add(disk);
    }

    public void RemoveDisk(NonPaperMedia disk)
    {
        if (HasDisk(disk.Id) == false)
            throw new InvalidOperationException(
                $"Диск (s/n: {disk.SerialNumber}) отсутствует в рабочей станции {InventoryNumber}.)");

        NonPaperMedia? removedDisk = Disks.FirstOrDefault(d => d.Id == disk.Id)
            ?? throw new InvalidOperationException(
                $"Диск не найден в рабочей станции {InventoryNumber}.)");

        Disks.Remove(removedDisk);
    }

    public void WriteOff() =>
        IsDecommissioned = true;

    public void ToRegister() =>
        IsDecommissioned = false;

    public bool HasDisk(NpmId id)
        => Disks.Any(d => d.Id == id);
}
