using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Users;

public class User : IChangedAt
{
    private User() { }

    public User(FullName fullName, string login)
    {
        FullName = fullName;
        Login = login;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public UserId Id { get; private set; }
    [Required]
    public FullName FullName { get; private set; }
    [Required, StringLength(20)]
    public string Login { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void UpdateFullName(FullName fullName)
    {
        ArgumentNullException.ThrowIfNull(fullName);

        FullName = fullName;
    }
}
