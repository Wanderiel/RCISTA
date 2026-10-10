using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.NPMs;

/// <summary>
/// Модель
/// </summary>
public class Model
{
    public Model(string name) =>
        Name = name;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ModelId Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; }
}
