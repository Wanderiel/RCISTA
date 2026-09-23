using Domain.Models.NPMs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Workstations;

public class Workstation
{
    private readonly List<NonPaperMedia> _disks;

    private Workstation() { }

    public Workstation(WorkstationId id, string inventory, List<NonPaperMedia> disks)
    {
        Id = id;
        Inventory = inventory;
        _disks = disks;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public WorkstationId Id { get; }
    [Required, StringLength(50)]
    public string Inventory { get; }
    public IReadOnlyList<NonPaperMedia> Disks => _disks;

    public void AddDisk(NonPaperMedia disk) =>
        _disks?.Add(disk);

    public void RemoveDisk(NonPaperMedia disk)
    {
        if (_disks.Contains(disk))
            _disks.Remove(disk);
    }
}
