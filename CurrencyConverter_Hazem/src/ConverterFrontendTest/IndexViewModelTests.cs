using ConverterFrontend.Models;
using Models;

namespace ConverterFrontendTest;

public class IndexViewModelTests
{
    private IndexViewModel _indexViewModel;

    [SetUp]
    public void Setup()
    {
        _indexViewModel = new IndexViewModel();
    }

    [Test]
    public void ConversionProperty_InitiallyNull()
    {
        Assert.IsNull(_indexViewModel.Conversion);
    }

    [Test]
    public void ConversionsProperty_InitiallyNull()
    {
        Assert.IsNull(_indexViewModel.Conversions);
    }

    [Test]
    public void ConversionProperty_CanBeSet()
    {
        var conversion = new CurrencyConversion();
        _indexViewModel.Conversion = conversion;

        Assert.That(conversion, Is.SameAs(_indexViewModel.Conversion));
    }

    [Test]
    public void ConversionsProperty_CanBeSet()
    {
        var conversions = new CurrencyConversion[] { new CurrencyConversion() };
        _indexViewModel.Conversions = conversions;

        Assert.That(conversions, Is.SameAs(_indexViewModel.Conversions));
    }
}