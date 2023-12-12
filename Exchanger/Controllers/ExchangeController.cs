using Exchanger.Application.Exchanges.Commands;
using Exchanger.Application.Exchanges.Queries;
using Exchanger.ExternalAPI.Responses;
using Exchanger.Services.Contracts;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Exchanger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeController
{
    private readonly IExchangeService _exchangeService;
    private readonly ISender _sender;

    public ExchangeController(ISender sender, IExchangeConverterFactory factory)
    {
        _exchangeService = factory.UseApiConverter();
        _sender = sender;
    }

    [HttpGet("convert", Name = "Convert")]
    [ProducesResponseType(typeof(ConvertResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Convert",
        Description = "Convert",
        OperationId = "Convert",
        Tags = new[]
        {
            "Exchange"
        })]
    public async Task<ConvertResponse> Convert(decimal amount, string currency)
    {
        var result = await _exchangeService.Convert(amount, currency);
        await _sender.Send(new CreateExchange(currency, amount, result.Amount));
        return result;
    }
    
    [HttpGet("history", Name = "History")]
    [ProducesResponseType(typeof(GetExchangesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(StatusCodeProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "History",
        Description = "History",
        OperationId = "History",
        Tags = new[]
        {
            "Exchange"
        })]
    public async Task<GetExchangesResponse> History(int page = 1, int pageSize = 10)
    {
        return await _sender.Send(new GetExchanges(page, pageSize));
    }
}