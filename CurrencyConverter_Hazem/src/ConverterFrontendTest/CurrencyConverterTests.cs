using ConverterFrontend;

namespace ConverterFrontendTest;

public class CurrencyConverterTests
{
    [Test]
    public void ConvertCurrency_ShouldReturnCorrectConversion_WhenValidCurrenciesAndAmountAreProvided()
    {
        // Arrange
        var converter = new CurrencyConverter();
        decimal amount = 100;
        string fromCurrency = "USD";
        string toCurrency = "EUR";

        // Act
        decimal result = converter.ConvertCurrency(amount, fromCurrency, toCurrency);

        // Assert
        Assert.That(result, Is.EqualTo(93));
    }

    [Test]
    public void ConvertCurrency_ShouldThrowException_WhenFromCurrencyIsNotSupported()
    {
        // Arrange
        var converter = new CurrencyConverter();
        decimal amount = 100;
        string fromCurrency = "XYZ"; // Unsupported currency
        string toCurrency = "EUR";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => converter.ConvertCurrency(amount, fromCurrency, toCurrency));
    }

    [Test]
    public void ConvertCurrency_ShouldThrowException_WhenToCurrencyIsNotSupported()
    {
        // Arrange
        var converter = new CurrencyConverter();
        decimal amount = 100;
        string fromCurrency = "USD";
        string toCurrency = "XYZ"; // Unsupported currency

        // Act & Assert
        Assert.Throws<ArgumentException>(() => converter.ConvertCurrency(amount, fromCurrency, toCurrency));
    }

    [Test]
    public void ConvertCurrency_ShouldReturnSameAmount_WhenFromAndToCurrencyAreSame()
    {
        // Arrange
        var converter = new CurrencyConverter();
        decimal amount = 100;
        string fromCurrency = "USD";
        string toCurrency = "USD";

        // Act
        decimal result = converter.ConvertCurrency(amount, fromCurrency, toCurrency);

        // Assert
        Assert.That(result, Is.EqualTo(amount));
    }
}