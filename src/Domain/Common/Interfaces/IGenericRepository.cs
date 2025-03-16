﻿namespace Domain.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
}
