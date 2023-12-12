using Exchanger.Application.Common.Interfaces;
using Exchanger.Domain.Entities;

namespace Exchanger.Application.Exchanges.Commands;

public record CreateExchange(string Currency, decimal Amount, decimal Result) : IRequest;

public class CreateExchangeValidator : AbstractValidator<CreateExchange>
{
    public CreateExchangeValidator()
    {
        RuleFor(v => v.Currency)
            .MaximumLength(3)
            .NotEmpty()
            .WithMessage("The currency must be non-empty.");
    }
}

public class CreateExchangeHandler : IRequestHandler<CreateExchange>
{
    private readonly IApplicationDbContext _context;

    public CreateExchangeHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateExchange request, CancellationToken cancellationToken)
    {
        var exchange = new Exchange()
        {
            Currency = request.Currency,
            Amount = request.Amount,
            Result = request.Result
        };

        await _context.Exchanges.AddAsync(exchange);
        await _context.SaveChangesAsync(cancellationToken);
    }
}