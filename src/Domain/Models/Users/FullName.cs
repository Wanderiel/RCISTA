namespace Domain.Models.Users;

public class FullName
{
    private FullName() { }

    private FullName(string lastName, string firstName, string patronymic)
    {
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
    }

    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string Patronymic { get; private set; }

    public static FullName Create(string lastName, string firstName, string patronymic)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(patronymic);

        return new FullName(lastName, firstName, patronymic);
    }
}
