using Domain.Models.NPMs;
using Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Workstations;

public class Workstation
{
    private Workstation() { }

    public Workstation(string inventory) =>
        Inventory = inventory;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public WorkstationId Id { get; }
    [Required, StringLength(50)]
    public string Inventory { get; }
    public UserId AutorId { get; private set; }
    public UserId ChangedId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public List<NonPaperMedia> Disks { get; private set; } = [];

    public void AddDisk(NonPaperMedia disk) =>
        Disks.Add(disk);

    public void RemoveDisk(NonPaperMedia disk) =>
        Disks.Remove(disk);
}
