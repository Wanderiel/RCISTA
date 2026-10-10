using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.NPMs;

public class NonPaperMedia : IChangedAt
{
    private NonPaperMedia() { }

    public NonPaperMedia(NpmType type, string manufacturer, Model model, string serialNumber, int capacity)
    {
        Type = type;
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        Capacity = capacity;
    }

    [Key]
    public NpmId Id { get; private set; }
    [Required]
    public NpmType Type { get; private set; }
    [Required, StringLength(100)]
    public string Manufacturer { get; private set; }
    [Required]
    public Model Model { get; private set; }
    /// <summary>
    /// Серийный номер
    /// </summary>
    [Required, StringLength(100)]
    public string SerialNumber { get; private set; }
    /// <summary>
    /// Ёмкость в Гб
    /// </summary>
    [Required]
    public int Capacity { get; private set; }
    [Required]
    public NpmStatus Status { get; private set; } = NpmStatus.Good;
    //public UserId AutorId { get; private set; }
    //public UserId ChangedId { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void ToBreak() =>
        Status = NpmStatus.Broken;

    public void Repair() =>
        Status = NpmStatus.Good;
}
