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

    public void AddDisks(List<NonPaperMedia> disks)
    {
        foreach (NonPaperMedia disk in disks)
        {
            if (HasDisk(disk.Id))
                throw new InvalidOperationException(
                    $"Диск (s/n: {disk.SerialNumber}) уже присуствует в рабочей станции {InventoryNumber}.)");
        }

        Disks.AddRange(disks);
    }

    public void RemoveDisks(List<NonPaperMedia> disks)
    {
        List<NonPaperMedia> removedDisks = new List<NonPaperMedia>();

        foreach (NonPaperMedia disk in disks)
        {
            NonPaperMedia? removedDisk = Disks.FirstOrDefault(d => d.Id == disk.Id)
                ?? throw new InvalidOperationException(
                    $"Диск не найден в рабочей станции {InventoryNumber}.)");

            removedDisks.Add(disk);
        }

        foreach (NonPaperMedia disk in removedDisks)
            Disks.Remove(disk);
    }

    public void WriteOff() =>
        IsDecommissioned = true;

    public void ToRegister() =>
        IsDecommissioned = false;

    public bool HasDisk(NpmId id)
        => Disks.Any(d => d.Id == id);
}
