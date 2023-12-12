using Exchanger.Services.Contracts;

namespace Exchanger.Services;

public class ExchangeConverterFactory : IExchangeConverterFactory
{
    private readonly List<IExchangeService> _services;

    public ExchangeConverterFactory(IEnumerable<IExchangeService> services)
    {
        _services = services.ToList();
    }

    public IExchangeService UseApiConverter()
    {
        return _services.FirstOrDefault(x => x.GetType() == typeof(ExchangeApiService));
    }
}