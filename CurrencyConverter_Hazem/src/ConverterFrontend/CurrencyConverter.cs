using Models;

namespace ConverterFrontend;

public class CurrencyConverter
{
    private readonly Dictionary<string, decimal> rates = new Dictionary<string, decimal>
    {
        {"USD", 1m},
        {"EUR", 0.93m},
        {"GBP", 0.76m},
        {"JPY", 130.53m},
        {"AUD", 1.31m}
    };
    
    public decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        if (!rates.ContainsKey(fromCurrency) || !rates.ContainsKey(toCurrency))
        {
            throw new ArgumentException("Unsupported currency.");
        }

        decimal rateToUSD = rates[fromCurrency];
        decimal amountInUSD = amount / rateToUSD;
        decimal targetRate = rates[toCurrency];
        return amountInUSD * targetRate;
    }
}