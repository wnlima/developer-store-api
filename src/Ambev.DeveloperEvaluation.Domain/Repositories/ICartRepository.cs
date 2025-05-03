using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<CartEntity> CreateAsync(CartEntity cart);
    Task<CartEntity?> GetByIdAsync(Guid id);
    Task<PaginatedList<CartEntity>> ListAsync(int pageNumber, int pageSize);
    Task UpdateAsync(CartEntity cart);
    Task DeleteAsync(Guid id);
}