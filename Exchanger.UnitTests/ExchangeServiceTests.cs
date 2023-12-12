using Exchanger.ExternalAPI.API;
using Exchanger.ExternalAPI.Responses;
using Exchanger.Options;
using Exchanger.Services;
using Exchanger.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace Exchanger.UnitTests;


public class ExchangeServiceTests
{
    private Mock<IOpenExchangeRatesAPI> _api;
    private IExchangeService _service;
    private LatestResponse _latestResponse;
    private OpenExchangeRatesOptions _options;
    
    [SetUp]
    public void SetUp()
    {
        _api = new Mock<IOpenExchangeRatesAPI>();
        _options = new OpenExchangeRatesOptions();
        _service = new ExchangeApiService(_api.Object, _options);
        _latestResponse = new LatestResponse()
        {
            Rates = new Dictionary<string, decimal>()
            {
                { "EUR", 0.9m },
                { "GBR", 1.1m },
            }
        };
    }

    [Test]
    public async Task Convert_WithMock_ShouldReturnResponse()
    {
        var currency = "EUR";
        var amount = 20m;
        _api.Setup(x => x.GetLatest(It.IsAny<string>())).ReturnsAsync(_latestResponse);

        var result = await _service.Convert(amount, currency);

        Assert.NotNull(result);
        Assert.Greater(result.Amount, 0);
        Assert.AreEqual(amount * 0.9m, result.Amount);
    }
}