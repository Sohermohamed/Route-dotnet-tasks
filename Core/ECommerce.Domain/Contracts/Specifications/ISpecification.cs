using ECommerce.Domain.Models;
using System.Linq.Expressions;


namespace ECommerce.Domain.Contracts.Specfications
{
    public interface ISpecification<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>> Criteria { get; } //where 

        List<Expression<Func<TEntity,Object>>> Includes { get; } 

        Expression<Func<TEntity, Object>> OrderBy { get; } 

        Expression<Func<TEntity, Object>> OrderByDesc { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; set; }

    }
}
