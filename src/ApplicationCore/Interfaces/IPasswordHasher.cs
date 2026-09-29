namespace ApplicationCore.Interfaces;

public interface IPasswordHasher
{
    string CreateHash(string password);
    bool Compare(string password, string hashedPassword);
}