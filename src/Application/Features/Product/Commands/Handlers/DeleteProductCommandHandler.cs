#nullable disable

using Application.Interfaces.Repositories;
using MediatR;
using Shared.Exceptions;

namespace Application.Features.Product.Commands.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new NotFoundException($"Product with ID {request.Id} was not found.");

        _unitOfWork.Product.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
