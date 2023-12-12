using Exchanger.ExternalAPI.Responses;

namespace Exchanger.Services.Contracts;

public interface IExchangeService
{
    Task<ConvertResponse> Convert(decimal amount, string currency);
}