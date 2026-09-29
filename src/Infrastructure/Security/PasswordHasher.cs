using ApplicationCore.Interfaces;
using Domain.Models.Exceptions;
using Infrastructure.Security.HashGenerators;

namespace Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int MINLENGTH = 8;

    public bool Compare(string password, string hash) =>
        Hash(password) == hash;

    public string CreateHash(string password)
    {
        Validate(password);

        return Hash(password);
    }

    private string Hash(string password) =>
        SHA256HashGenerator.Compute(password);

    private static void Validate(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidPasswordException($"Пароль не должен быть пустым");

        if (password.Length < MINLENGTH)
            throw new InvalidPasswordException($"Пароль должен быть не менее {MINLENGTH} символов");
    }
}
