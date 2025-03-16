using MediatR;

namespace Application.Features.Product.Commands;

public record DeleteProductCommand(int Id) : IRequest;
