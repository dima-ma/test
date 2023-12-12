using Exchanger.ExternalAPI.Responses;
using Refit;

namespace Exchanger.ExternalAPI.API;

public interface IOpenExchangeRatesAPI
{
    [Get("/latest.json?app_id={appId}")]
    Task<LatestResponse> GetLatest(string appId);
}

