using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.NPMs;

public class NonPaperMedia : IChangedAt
{
    public NonPaperMedia(string type, string manufacturer, string model, string serialNumber, int capacity)
    {
        Type = type;
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        Capacity = capacity;
    }

    /// <summary>
    /// Id для базы данных
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public NpmId Id { get; private set; }
    /// <summary>
    /// Тип: HDD, SSD
    /// </summary>
    [Required]
    public string Type { get; private set; }
    /// Производитель
    [Required, StringLength(50)]
    public string Manufacturer { get; private set; }
    /// <summary>
    /// Модель
    /// </summary>
    [Required, StringLength(50)]
    public string Model { get; private set; }
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
    /// <summary>
    /// Статус состояния: неработоспособный - true; рабочий - false
    /// </summary>
    [Required]
    public bool IsBroken { get; private set; } = false;
    //public UserId AutorId { get; private set; }
    //public UserId ChangedId { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void UpdateType(string type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(type, nameof(type));

        Type = type;
    }

    public void UpdateManufacturer(string manufacturer)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(manufacturer, nameof(manufacturer));

        Manufacturer = manufacturer;
    }

    public void UpdateModel(string model)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(model, nameof(model));

        Model = model;
    }

    public void UpdateSerialNumber(string serialNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(serialNumber, nameof(serialNumber));

        SerialNumber = serialNumber;
    }

    public void UpdateCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity, nameof(capacity));

        Capacity = capacity;
    }

    public void ToBreak() =>
        IsBroken = true;

    public void Repair() =>
        IsBroken = false;
}
