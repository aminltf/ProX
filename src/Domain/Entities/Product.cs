#nullable disable

using Domain.Common.Abstractions;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product()
    {
        
    }
}
