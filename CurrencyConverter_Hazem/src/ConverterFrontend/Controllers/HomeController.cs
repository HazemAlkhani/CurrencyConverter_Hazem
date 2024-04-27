using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Models;
using ConverterFrontend.Models;
using FeatureHubSDK;

namespace ConverterFrontend.Controllers;

public class HomeController : Controller
{
   
    private readonly HttpClient _httpClient;
    private readonly EdgeFeatureHubConfig? _featureHubConfig;
    private bool _historyEnabled;

    public HomeController(EdgeFeatureHubConfig? featureHubConfig)
    {
        _httpClient = new HttpClient();
        _featureHubConfig = featureHubConfig;
        _httpClient.BaseAddress = new Uri("http://converter-api:8080/");
    }

    private async Task<bool> GetHistoryEnabled()
    {
        var fh = await _featureHubConfig.NewContext().Build();
        return fh["history"].IsEnabled;
    }

    private async Task<CurrencyConversion[]> GetConversions()
    {
        var result = await _httpClient.GetAsync(_httpClient.BaseAddress + "currencyconverter");
        return await result.Content.ReadFromJsonAsync<CurrencyConversion[]>() ?? Array.Empty<CurrencyConversion>();
    }

    public async Task<IActionResult>? Index()
    {
        _historyEnabled = await GetHistoryEnabled();
        var conversions = await GetConversions();
        var model = new IndexViewModel
        {
            Conversions = conversions,
            Conversion = null,
            HistoryEnabled = _historyEnabled
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ConvertCurrency(string source, string target, int value)
    {
        var convertedValue = new CurrencyConverter().ConvertCurrency(value, source, target);
        var conversion = new CurrencyConversion(DateTime.Now, source, target, value, convertedValue);

        var content = new StringContent(JsonSerializer.Serialize(conversion), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(_httpClient.BaseAddress + "currencyconverter", content);

        _historyEnabled = await GetHistoryEnabled();
        var conversions = await GetConversions();
        return View("Index", new IndexViewModel { Conversions = conversions, Conversion = conversion, HistoryEnabled = _historyEnabled });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}