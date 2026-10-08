using System.Security.Cryptography;
using System.Text;

namespace Pesedjet.Client.Utilities;

public static class SecurityUtilities
{
    public static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}