using System.ComponentModel.DataAnnotations.Schema;

namespace Exchanger.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
}
