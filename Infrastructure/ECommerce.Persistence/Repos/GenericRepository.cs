using ECommerce.Domain.Contracts.Pepos;
using ECommerce.Domain.Models;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repos
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext context) : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()=>await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)=> await context.Set<TEntity>().FindAsync(id);

        public void AddAsync(TEntity entity)=>context.Set<TEntity>().Add(entity);

        public void UpdateAsync(TEntity entity)=> context.Set<TEntity>().Update(entity);    

        public void DeleteAsync(TEntity entity)=>context.Set<TEntity>().Remove(entity);
       
    }
}
