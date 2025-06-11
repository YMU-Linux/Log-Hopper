using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace api.Database
{
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
        public required string Password { get; set; }
    }

    public class DatabaseManager
    {
        public required string ConnectionServer { get; set; }
        public required string DatabaseName { get; set; }

        private readonly List<User> _databaseUsersList = new();

        public List<string> GetDatabaseUsers()
        {
            return _databaseUsersList
                .Select(user => user.UseCase)
                .ToList();
        }

        public void AddUser(string useCase, string username, string password)
        {
            _databaseUsersList.Add(new User
            {
                UseCase = useCase,
                UserID = username,
                Password = password
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
        public async Task ConnectWithUser(User userObj, string dbCommand, string[] parameters)
        {
            if (!_databaseUsersList.Any(u => u.UserID == userObj.UserID && u.Password == userObj.Password))
            {
                throw new UnauthorizedAccessException("User not found or incorrect credentials.");
            }

            var connString = $"Server={ConnectionServer};User ID={userObj.UserID};Password={userObj.Password};Database={DatabaseName}";

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
