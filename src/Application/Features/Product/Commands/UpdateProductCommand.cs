using Application.Features.Product.DTOs;
using MediatR;

namespace Application.Features.Product.Commands;

public record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<ProductDTO>;
