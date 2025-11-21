using ECommerce.Domain.Contracts.Specfications;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Contracts.Repos
{
    public interface IGenericRebosatory<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
       public Task<IEnumerable<TEntity>> GetAllAsync(); 
       public Task<TEntity> GetByIdAsync(TKey id);
       public void Add(TEntity entity);
       public void Update(TEntity entity);
       public void Delete(TEntity entity);
       public Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity,TKey> specification);
       public Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity, TKey> specification);
       public Task<int> GetCountWithSpecificationsAsync(ISpecification<TEntity, TKey> specification);

    }
}
