using System.Security.Cryptography;
using System.Text;

namespace TrelloClone.Helper
{

    public class Helper
    {

        private static readonly byte[] salt = Encoding.UTF8.GetBytes("TrelloSalt");

        const int keySize = 64;
        const int iterations = 350;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashString(string inputString)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(inputString),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public static string generateToken(string username)
        {
            return string.Format("{0}_{1:N}", username, Guid.NewGuid());
        }
        // Checks if the input contains illegal characters
        // Returns true if string is illegal, false if its okay.
        public static bool illegalStringCheck(string inputString)
        {
            // CHECK STRING

            return false;
        }

        public const string authorizedBoardsClaimName = "authorizedBoards";

    }
}
