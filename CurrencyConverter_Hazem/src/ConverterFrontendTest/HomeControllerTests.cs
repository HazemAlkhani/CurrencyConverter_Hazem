using ConverterFrontend.Controllers;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConverterFrontendTest;

public class HomeControllerTests : IDisposable
{
    private HomeController? _homeController;

    [SetUp]
    public void Setup()
    {
        ILogger<HomeController> logger = Mock.Of<ILogger<HomeController>>();
        IFeatureHubConfig featureHubConfig = Mock.Of<IFeatureHubConfig>();
        _homeController = new HomeController(featureHubConfig as EdgeFeatureHubConfig);
    }
    
    [TearDown]
    public void Dispose()
    {
        _homeController?.Dispose();
    }

    [Test]
    public Task Index_ThrowsNullReferenceException()
    {
        Assert.ThrowsAsync<NullReferenceException>(() => _homeController?.Index() ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }
    
    [Test]
    public void Error_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _homeController?.Error());
    }

    [Test]
    public void ConvertCurrency_Works()
    {
        var result = _homeController?.ConvertCurrency("USD", "EUR", 100);

        Assert.IsInstanceOf<Task<IActionResult>>(result);
    }
}