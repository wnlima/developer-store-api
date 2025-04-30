using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<CartEntity> CreateAsync(CartEntity cart);
    Task<CartEntity?> GetByIdAsync(Guid id);
    Task<IPaginatedList<CartEntity>> ListAsync(int pageNumber, int pageSize);
    Task UpdateAsync(CartEntity cart);
    Task DeleteAsync(Guid id);
}