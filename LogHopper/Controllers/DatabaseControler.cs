using MySqlConnector;
using System;
using System.Data;
using System.Threading.Tasks;

public class MariaDBController
{
    private readonly string _connectionString;

    public MariaDBController(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <summary>
    /// Executes a SELECT query and returns a DataTable asynchronously.
    /// </summary>
    public async Task<DataTable> ReadAsync(string query, params MySqlParameter[] parameters)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Query cannot be null or empty.", nameof(query));

        DataTable dt = new DataTable();

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new MySqlCommand(query, connection);
        if (parameters != null && parameters.Length > 0)
            cmd.Parameters.AddRange(parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        dt.Load(reader);

        return dt;
    }

    /// <summary>
    /// Executes an INSERT, UPDATE, or DELETE query asynchronously.
    /// Returns the number of affected rows.
    /// </summary>
    public async Task<int> WriteAsync(string query, params MySqlParameter[] parameters)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Query cannot be null or empty.", nameof(query));

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var cmd = new MySqlCommand(query, connection);
        if (parameters != null && parameters.Length > 0)
            cmd.Parameters.AddRange(parameters);

        return await cmd.ExecuteNonQueryAsync();
    }
}


