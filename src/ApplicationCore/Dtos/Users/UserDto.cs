namespace ApplicationCore.Dtos.Users;

public class UserDto
{
    public required string Login { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Patronymic { get; set; }
}
