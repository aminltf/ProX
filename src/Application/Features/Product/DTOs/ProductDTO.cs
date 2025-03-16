#nullable disable

using FluentValidation;

namespace Application.Features.Product.DTOs;

public record ProductDTO(int Id, string Name, decimal Price);

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Price).NotEmpty();
    }
}
