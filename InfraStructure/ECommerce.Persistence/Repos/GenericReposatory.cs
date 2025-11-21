using ECommerce.Domain.Contracts.Repos;
using ECommerce.Domain.Models;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Contracts.Specfications;

namespace ECommerce.Persistence.Repos
{
    public class GenericReposatory<TEntity, TKey>(StoreDbContext context) : IGenericRebosatory<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();
        public async Task<TEntity> GetByIdAsync(TKey id) => await context.Set<TEntity>().FindAsync(id);
        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);
        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).ToListAsync();
           
        }

        public async Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecificationsAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).CountAsync();
        }
    }
}
