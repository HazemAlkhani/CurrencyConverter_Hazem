using Models;
using System.Threading.Tasks;

namespace ConverterAPI
{
    public interface IDatabaseService
    {
        Task<CurrencyConversion[]> GetConversionsAsync();
        Task SaveConversionAsync(CurrencyConversion conversion);
        Task<CurrencyConversion> GetConversionByIdAsync(int id);
        Task UpdateConversionAsync(CurrencyConversion conversion);
        Task DeleteConversionAsync(int id);
    }
}