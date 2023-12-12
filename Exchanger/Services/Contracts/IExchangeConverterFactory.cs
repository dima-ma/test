namespace Exchanger.Services.Contracts;

public interface IExchangeConverterFactory
{
    public IExchangeService UseApiConverter();
}