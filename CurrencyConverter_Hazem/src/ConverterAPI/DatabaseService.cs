using Dapper;
using Models;
using MySqlConnector;

namespace ConverterAPI
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<CurrencyConversion[]> GetConversionsAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var conversions = await connection.QueryAsync<CurrencyConversion>(
                "SELECT `date`, source, target, `value`, result FROM Conversions ORDER BY `date` DESC");
            return conversions.ToArray();
        }

        public async Task SaveConversionAsync(CurrencyConversion conversion)
        {
            if (conversion == null)
                throw new ArgumentNullException(nameof(conversion), "Conversion object cannot be null.");

            if (conversion.Value <= 0)
            {
                throw new ArgumentException("Value must be greater than 0", nameof(conversion));
            }

            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(@"
        INSERT INTO Conversions (`date`, source, target, `value`, result) 
        VALUES (@Date, @Source, @Target, @Value, @Result)", conversion);
        }

        public async Task<CurrencyConversion> GetConversionByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CurrencyConversion>(
                "SELECT `date`, source, target, `value`, result FROM Conversions WHERE id = @Id", new { Id = id }) ??
                   throw new InvalidOperationException();
        }

        public async Task UpdateConversionAsync(CurrencyConversion conversion)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(@"
                UPDATE Conversions 
                SET `date` = @Date, source = @Source, target = @Target, `value` = @Value, result = @Result 
                WHERE id = @Id", conversion);
        }

        public async Task DeleteConversionAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync("DELETE FROM Conversions WHERE id = @Id", new { Id = id });
        }
    }
}
