namespace ApplicationCore.Interfaces;

public interface INpmLookup
{
    Task<bool> HasNpmBySerialnumber(string serialnumber, CancellationToken cancellationToken = default);
}
