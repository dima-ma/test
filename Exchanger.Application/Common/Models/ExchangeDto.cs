using Exchanger.Application.Common.Mappings;
using Exchanger.Domain.Entities;

namespace Exchanger.Application.Common.Models;

public class ExchangeDto : IMapFrom<Exchange>
{
    public string Currency { get; set; }
    public string Result { get; set; }
}