using Domain.Models.NPMs;

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

    public WorkstationId Id { get; }
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
