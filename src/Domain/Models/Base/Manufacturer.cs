using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Base;

public class Manufacturer
{
    public Manufacturer(string name) =>
        Name = name;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ManufacturerId ManufacturerId { get; set; }
    [Required, StringLength(100)]
    public string Name { get; private set; }
}
