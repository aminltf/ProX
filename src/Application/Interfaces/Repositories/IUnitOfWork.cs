using Domain.Common.Interfaces;

namespace Application.Interfaces.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;

    IProductRepository Product { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
