using System.Text;

namespace PasswordManager
{
    // the eneteries stored in the structure of -> website = password (Key-Value pair),
    // so the best data structure is Dictionary.
    internal class Program
    {
        private static readonly Dictionary<string, string> _passwordEnteries = new();
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your password : ");
            string appPassword = Console.ReadLine().Trim();
            if (appPassword == "GOOD!")
            {
                ReadPasswords();
                while (true)
                {
                    Console.WriteLine("Please select an option : ");
                    Console.WriteLine("1. List all paswords");
                    Console.WriteLine("2. Add/Change passwords");
                    Console.WriteLine("3. Get password");
                    Console.WriteLine("4. Delete password");

                    var selectedOption = Console.ReadLine().Trim();
                    if (selectedOption == "1")
                        ListAllPassword();
                    else if (selectedOption == "2")
                        AddOrChangePassword();
                    else if (selectedOption == "3")
                        GetPassword();
                    else if (selectedOption == "4")
                        DeletePassword();
                    else
                        Console.WriteLine("Invalid option !");

                    Console.WriteLine("--------------------------------------------");
                }

            }
            else
            {
                Console.WriteLine("Invalid Password. ");
            }
        }

        private static void ListAllPassword()
        {
            foreach (var entry in _passwordEnteries)
                Console.WriteLine($"{entry.Key} = {entry.Value}");
        }

        private static void AddOrChangePassword()
        {
            Console.WriteLine("Please enter website/app name: ");
            var appName = Console.ReadLine().Trim();
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine().Trim();

            if (_passwordEnteries.ContainsKey(appName))
                _passwordEnteries[appName] = password;
            else
                _passwordEnteries.Add(appName, password);
            SavePassword();
        }

        private static void GetPassword()
        {
            Console.WriteLine("Please enter website/app name: ");
            var appName = Console.ReadLine().Trim();
            if (_passwordEnteries.ContainsKey(appName))
                Console.WriteLine($"Your password is : {_passwordEnteries[appName]}");
            else
                Console.WriteLine("Password not found.");
        }

        private static void DeletePassword()
        {
            Console.WriteLine("Please enter website/app name: ");
            var appName = Console.ReadLine().Trim();
            if (_passwordEnteries.ContainsKey(appName))
                _passwordEnteries.Remove(appName);
            else
                Console.WriteLine("Password not found.");
            SavePassword();
        }

        private static void ReadPasswords()
        {
            if (File.Exists("password.txt"))
            {
                var passwordLine = File.ReadAllText("password.txt");
                foreach (var line in passwordLine.Split(Environment.NewLine)) // Environment.NewLine to split the passwordLine string by the line.
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('=');
                        var appName = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);

                        _passwordEnteries.Add(appName, EncryptionUtility.Decrypt(password)); // U must decrypt the password before using
                    }
                }        
            }
            else
            {
                Console.WriteLine("File not found.");
            }
                
 
        }

         // We use SavePassword function, because if u close the program this function will save all passwords
         // that exist in dict in text file. -> without this func u will loss the data once the program closed.

        private static void SavePassword()
        {
            var sb = new StringBuilder();
            foreach(var entry in _passwordEnteries)
                sb.AppendLine($"{entry.Key}={EncryptionUtility.Encrypt(entry.Value)}");  // U must Encrypt the password before save it.
            File.WriteAllText("password.txt", sb.ToString());

        }
    }
}