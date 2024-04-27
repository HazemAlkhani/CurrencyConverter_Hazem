using Models;

namespace ConverterFrontend.Models;

public class IndexViewModel
{
    public CurrencyConversion? Conversion { get; set; }
    public CurrencyConversion[]? Conversions { get; set; }
    public bool HistoryEnabled { get; set; }
}