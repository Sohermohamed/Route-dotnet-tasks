using ECommerce.Domain.Contracts.Pepos;
using ECommerce.Domain.Contracts.UOW;
using ECommerce.Domain.Models;
using ECommerce.Persistence.Contexts;
using ECommerce.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.UOW
{
   public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _Repos = [];
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            if (_Repos.ContainsKey(TypeName))
            {
                return (IGenaricRepository<TEntity, TKey>)_Repos[TypeName];
            }
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(context);
                _Repos.Add(TypeName, Repo);
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
