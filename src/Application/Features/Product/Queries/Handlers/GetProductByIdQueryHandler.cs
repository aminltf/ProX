#nullable disable

using Application.Features.Product.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Exceptions;

namespace Application.Features.Product.Queries.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new NotFoundException($"Product with ID {request.Id} was not found.");
        return _mapper.Map<ProductDTO>(product);
    }
}
