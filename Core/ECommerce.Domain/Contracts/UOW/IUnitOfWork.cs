using ECommerce.Domain.Contracts.Pepos;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.UOW
{
   public interface IUnitOfWork
    {
       IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task <int> SaveChangesAsync();


    }
}
