using Application.Features.Product.Commands;
using Application.Features.Product.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>();

        CreateMap<CreateProductCommand, Product>();

        CreateMap<UpdateProductCommand, Product>();

        CreateMap<DeleteProductCommand, Product>();
    }
}
