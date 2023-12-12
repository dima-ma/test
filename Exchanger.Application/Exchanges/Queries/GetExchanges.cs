using Exchanger.Application.Common.Interfaces;
using Exchanger.Application.Common.Mappings;
using Exchanger.Application.Common.Models;
using Exchanger.Domain.Entities;

namespace Exchanger.Application.Exchanges.Queries;

public record GetExchanges(int Page, int PageSize) : IRequest<GetExchangesResponse>;

public class GetExchangesHandler: IRequestHandler<GetExchanges, GetExchangesResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExchangesHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetExchangesResponse> Handle(GetExchanges request, CancellationToken cancellationToken)
    {
        var exchanges = await _context.Exchanges
            .ProjectTo<ExchangeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.Page, request.PageSize);
        
        return new GetExchangesResponse(exchanges);
    }
}

public record GetExchangesResponse(PaginatedList<ExchangeDto> Exchanges);