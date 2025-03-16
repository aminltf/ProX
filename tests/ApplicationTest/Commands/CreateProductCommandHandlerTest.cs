#nullable disable

using Application.Features.Product.Commands.Handlers;
using Application.Features.Product.Commands;
using Application.Features.Product.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using FluentAssertions;

namespace ApplicationTest.Commands;

public class CreateProductCommandHandlerTest
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTest()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CreateProductCommandHandler(
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Create_Product()
    {
        var command = new CreateProductCommand { Name = "Test Product", Price = 100 };
        var product = new Product { Name = command.Name, Price = command.Price };
        var productDto = new ProductDTO(1, "Test Product", 100);

        _mapperMock.Setup(m => m.Map<Product>(command)).Returns(product);
        _repositoryMock.Setup(r => r.AddAsync(product, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test Product");
        result.Price.Should().Be(100);
    }
}
