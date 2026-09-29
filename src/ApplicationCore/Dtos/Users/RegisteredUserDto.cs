namespace ApplicationCore.Dtos.Users;

public class RegisteredUserDto
{
    public required string Login { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Patronymic { get; set; }
    public required string Password1 { get; set; }
    public required string Password2 { get; set; }
}
