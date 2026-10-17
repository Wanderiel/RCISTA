using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Base;

/// <summary>
/// Производитель
/// </summary>
public class Manufacturer
{
    public Manufacturer(string name) =>
        Name = name;

    public ManufacturerId Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; private set; }
}
