#nullable disable

using Application.Features.Product.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Exceptions;

namespace Application.Features.Product.Commands.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mepper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mepper = mapper;
    }

    public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new NotFoundException($"Product with ID {request.Id} was not found.");

        product.Name = request.Name;
        product.Price = request.Price;
        _unitOfWork.Product.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mepper.Map<ProductDTO>(product);
    }
}
