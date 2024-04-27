namespace Models;

public record CurrencyConversion(DateTime Date, string Source, string Target, int Value, decimal Result)
{
    public CurrencyConversion() : this(DateTime.Now, "", "", 0, 0) { }
}