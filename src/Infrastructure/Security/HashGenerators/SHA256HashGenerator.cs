using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security.HashGenerators;

public class SHA256HashGenerator
{
    private static readonly SHA256 s_sHA256 = SHA256.Create();

    public static string Compute(string toHash)
    {
        if (string.IsNullOrEmpty(toHash))
            throw new ArgumentNullException(nameof(toHash));

        byte[] toHashBytes = Encoding.UTF8.GetBytes(toHash);
        byte[] hashValue = s_sHA256.ComputeHash(toHashBytes);

        return Convert.ToBase64String(hashValue);
    }
}
