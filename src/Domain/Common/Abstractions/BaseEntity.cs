using Domain.Common.Interfaces;

namespace Domain.Common.Abstractions;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}
