using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.NPMs;

public class NpmModel
{
    public NpmModel(string name) =>
        Name = name;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    NpmModelId Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; }
}
