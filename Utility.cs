using System.Text;
using System.Security.Cryptography;
namespace HW3
{
    public static class Utility
    {
        public static string GenerateHash(string str)
        {
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }

        public static bool VerifyHash(string str, string hash)
        {
            if (GenerateHash(str) == hash) return true;
            return false;
        }
    }
}
