namespace ApplicationCore.Interfaces;

public interface IUserLookup
{
    Task<bool> HasUserByLogin(string username, CancellationToken cancellationToken = default);
}
