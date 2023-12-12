using Exchanger.ExternalAPI.API;
using Exchanger.ExternalAPI.Responses;
using Exchanger.Options;
using Exchanger.Services.Contracts;

namespace Exchanger.Services;

public class ExchangeApiService : IExchangeService
{
    private readonly IOpenExchangeRatesAPI _api;
    private readonly OpenExchangeRatesOptions _options;

    public ExchangeApiService(IOpenExchangeRatesAPI api, OpenExchangeRatesOptions options)
    {
        _api = api;
        _options = options;
    }

    public async Task<ConvertResponse> Convert(decimal amount, string currency)
    {
        var latest = await _api.GetLatest(_options.AppId);
        var rate = latest.Rates[currency];
        var result = amount * rate;
        return new ConvertResponse()
        {
            Amount = result,
            From = "USD",
            To = currency
        };
    }
}