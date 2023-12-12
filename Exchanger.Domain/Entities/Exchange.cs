using Exchanger.Domain.Common;

namespace Exchanger.Domain.Entities;

public class Exchange : BaseAuditableEntity
{
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public decimal Result { get; set; }
}