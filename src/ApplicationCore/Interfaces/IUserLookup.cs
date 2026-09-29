namespace ApplicationCore.Interfaces;

public interface IUserLookup
{
    Task<bool> HasUserByLoginAsync(string username);
}
