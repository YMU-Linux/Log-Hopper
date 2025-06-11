using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Security.Cryptography;

namespace api.Database
{
    public class CryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("your-encryption-key"); // Replace with your actual key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("your-initialization-vector"); // Replace with your actual IV
        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;


            var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);

        }

        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }


    public enum Level
    {
        Read,
        Write,
        DropTable,
        CreateTable,
        Admin
    }

    public record User
    {
        public required string UseCase { get; set; }
        public required string UserID { get; set; }
        public required string EncryptedPassword { get; set; }
    }


    public class DatabaseManager
    {
        public required string ConnectionServer { get; set; }
        public required string DatabaseName { get; set; }

        private readonly List<User> _databaseUsersList = new();


        public List<string> GetDatabaseUsers()
        {
            return _databaseUsersList
                .Select(user => user.UserID)
                .ToList();
        }

        public void AddUser(string useCase, string username, string password)
        {
            var encryptedPassword = CryptoHelper.Encrypt(password);

            _databaseUsersList.Add(new User
            {
                UseCase = useCase,
                UserID = username,
                EncryptedPassword = encryptedPassword
            });
        }


        public void RemoveUserAt(int index)
        {
            if (index < 0 || index >= _databaseUsersList.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            _databaseUsersList.RemoveAt(index);
        }

        /// <summary>
        /// Connects with a user and executes a SQL command.
        /// </summary>
        /// <param name="userObj">User credentials</param>
        /// <param name="dbCommand">SQL command string</param>
        /// <param name="parameters">["param1=value", "param2=value"]</param>
        public async Task ConnectWithUser(int userIndex, string dbCommand, string[] parameters)
        {
            var userObj = _databaseUsersList.ElementAtOrDefault(userIndex);
            if (userObj == null)
            {
                throw new ArgumentException("User not found at the specified index.");
            }

            if (!_databaseUsersList.Any(u => u.UserID == userObj.UserID && u.EncryptedPassword == userObj.EncryptedPassword))
            {
                throw new UnauthorizedAccessException("User not found or incorrect credentials.");
            }

            var decryptedPassword = CryptoHelper.Decrypt(userObj.EncryptedPassword);
            var connString = $"Server={ConnectionServer};User ID={userObj.UserID};Password={decryptedPassword};Database={DatabaseName}";


            await using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            await using var cmd = new MySqlCommand(dbCommand, connection);

            foreach (var param in parameters)
            {
                var parts = param.Split('=', 2);
                if (parts.Length != 2)
                    throw new ArgumentException($"Invalid parameter format: '{param}'");

                cmd.Parameters.AddWithValue("@" + parts[0], parts[1]);
            }

            if (dbCommand.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader[0]); // You could collect or return data here
                }
            }
            else
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
