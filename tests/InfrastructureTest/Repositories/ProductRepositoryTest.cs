#nullable disable

using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTest.Repositories;

public class ProductRepositoryTest
{
    private readonly ApplicationContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        _context = new ApplicationContext(options);
        _repository = new ProductRepository(_context);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Product_To_Database()
    {
        var product = new Product { Name = "Test Product", Price = 100 };

        await _repository.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync();

        var savedProduct = await _context.Products.FirstOrDefaultAsync();
        savedProduct.Should().NotBeNull();
        savedProduct.Name.Should().Be("Test Product");
        savedProduct.Price.Should().Be(100);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product_When_Exists()
    {
        var product = new Product { Name = "Test Product", Price = 100 };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(product.Id, CancellationToken.None);
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Product");
    }

    [Fact]
    public async Task Delete_Should_Remove_Product_From_Database()
    {
        var product = new Product { Name = "Test Product", Price = 100 };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        _repository.Delete(product);
        await _context.SaveChangesAsync();

        var deletedProduct = await _context.Products.FindAsync(product.Id);
        deletedProduct.Should().BeNull();
    }
}
