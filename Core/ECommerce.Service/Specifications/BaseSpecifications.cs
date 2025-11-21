using ECommerce.Domain.Contracts.Specfications;
using ECommerce.Domain.Models;
using System.Linq.Expressions;

namespace ECommerce.Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
    {
        #region Where
        protected BaseSpecifications(Expression<Func<TEntity, bool>> _Criteria)
        {
            Criteria = _Criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; } 
        #endregion

        #region Ordering
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> _OrderBy)
        {
            OrderBy = _OrderBy;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> _OrderByDesc)
        {
            OrderByDesc = _OrderByDesc;
        } 
        #endregion

        #region Includes
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        protected void AddIncludes(Expression<Func<TEntity, object>> IncludeExperssion)
        {
            Includes.Add(IncludeExperssion);
        }
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; set; }

        protected void ApplyPagination(int PageIndex, int PageSize)
        {
            Skip = (PageIndex - 1) * PageSize;
            Take = PageSize;
            IsPagingEnabled = true;
        }
        #endregion

    }
}
