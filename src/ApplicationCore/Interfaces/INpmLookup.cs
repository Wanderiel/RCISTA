namespace ApplicationCore.Interfaces;

public interface INpmLookup
{
    Task<bool> HasNpmBySerialnumberAsync(string serialnumber, CancellationToken cancellationToken = default);
}
