using ConverterAPI;
using Models;

namespace ConverterAPITest
{
    public class DatabaseServiceTests
    {
        private IDatabaseService _databaseService;

        [SetUp]
        public void Setup()
        {
            // Initialize the database service with a connection string
            string connectionString = "Server=localhost;Port=3306;Database=converter;Uid=root;Pwd=mypassword;";

            _databaseService = new DatabaseService(connectionString);
        }

        [Test]
        public async Task GetConversionsAsync_ReturnsConversions()
        {
            // Act
            var conversions = await _databaseService.GetConversionsAsync();

            // Assert
            Assert.IsNotNull(conversions);
            Assert.IsInstanceOf<CurrencyConversion[]>(conversions);
            Assert.IsTrue(conversions.Length > 0);
        }

        [Test]
        public async Task SaveConversionAsync_SavesConversion()
        {
            // Arrange
            var conversion = new CurrencyConversion
            {
                Date = DateTime.UtcNow,
                Source = "USD",
                Target = "EUR",
                Value = 100,
                Result = 85.25M // Use 'M' suffix to specify decimal literal

            };
            
            
            
            await _databaseService.SaveConversionAsync(conversion);
            
        }
    }
}