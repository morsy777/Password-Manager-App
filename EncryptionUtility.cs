using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class EncryptionUtility
    {
        // We defined the variable as static varss because the Encrypt & Decrypt are static func that's mean they can't access instance fields.

        private static readonly string _originalChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string _altChars = "F2bDHi4YVQu7TUxhmfsq6tkXWl10ZNy9BnSAPIvjgKw3CrzReLEdJ5caO8MGop";

        public static string Encrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (var ch in password)
            {
                var charIndex = _originalChars.IndexOf(ch);
                sb.Append(_altChars[charIndex]);
            }
            return sb.ToString();
        }
        public static string Decrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (var ch in password)
            {
                var charIndex = _altChars.IndexOf(ch);
                sb.Append(_originalChars[charIndex]);
            }
            return sb.ToString();
        }
    }
}
