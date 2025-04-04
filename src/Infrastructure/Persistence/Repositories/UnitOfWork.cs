﻿#nullable disable

using Application.Interfaces.Repositories;
using Domain.Common.Interfaces;
using Infrastructure.Persistence.Contexts;
using System.Collections;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationContext context, IProductRepository productRepository)
    {
        _context = context;
        Product = productRepository;
    }

    public IProductRepository Product { get; }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<T>);
            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
