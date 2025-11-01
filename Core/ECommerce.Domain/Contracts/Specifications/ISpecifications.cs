using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Specifications
{
   public class ISpecifications<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<Func<TEntity, object>> OrderBy { get; }
        List<Func<TEntity, object>> OrderByDesc { get; }



    }
}
