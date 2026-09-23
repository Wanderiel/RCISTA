using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.NPMs;

public class NonPaperMedia
{
    public NonPaperMedia(string type, string manufacturer, string model, string serialNumber, int capacity)
    {
        Type = type;
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        Capacity = capacity;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public NpmId Id { get; private set; }
    [Required]
    public string Type { get; private set; }
    [Required, StringLength(50)]
    public string Manufacturer { get; private set; }
    [Required, StringLength(50)]
    public string Model { get; private set; }
    [Required, StringLength(100)]
    public string SerialNumber { get; private set; }
    [Required]
    public int Capacity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void UpdateType(string type)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(type, nameof(type));

        Type = type;
    }

    public void UpdateManufacturer(string manufacturer)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(manufacturer, nameof(manufacturer));

        Manufacturer = manufacturer;
    }

    public void UpdateSerialNumber(string serialNumber)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(serialNumber, nameof(serialNumber));

        SerialNumber = serialNumber;
    }

    public void UpdateCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity, nameof(capacity));

        Capacity = capacity;
    }
}
