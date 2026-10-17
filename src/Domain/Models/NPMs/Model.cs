using System.ComponentModel.DataAnnotations;

namespace Domain.Models.NPMs;

public class Model
{
    private Model() { }

    public Model(string name) =>
        Name = name;

    public ModelId Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; }
}
