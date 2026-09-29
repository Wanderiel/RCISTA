namespace Domain.Models.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string? reason = null)
        : base(BuildMessage(reason))
    { }

    private static string BuildMessage(string? reason) =>
        reason is null
            ? $"Invalid password."
            : $"Invalid password: {reason}.";
}
