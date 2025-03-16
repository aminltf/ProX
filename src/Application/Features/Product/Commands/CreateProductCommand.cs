#nullable disable

using Application.Features.Product.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Features.Product.Commands;

public record CreateProductCommand : IRequest<ProductDTO>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        
    }
}
