namespace Exchanger.ExternalAPI.Responses;

public class ConvertResponse
{
    public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }
}