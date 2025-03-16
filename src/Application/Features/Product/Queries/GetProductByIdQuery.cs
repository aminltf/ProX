using Application.Features.Product.DTOs;
using MediatR;

namespace Application.Features.Product.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDTO>;
