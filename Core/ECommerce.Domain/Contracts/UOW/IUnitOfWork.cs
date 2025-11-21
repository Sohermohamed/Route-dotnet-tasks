using ECommerce.Domain.Contracts.Repos;
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
        IGenericRebosatory<TEntity , TKey> GetRebosatory<TEntity , TKey>() where TEntity:BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
    }
}
