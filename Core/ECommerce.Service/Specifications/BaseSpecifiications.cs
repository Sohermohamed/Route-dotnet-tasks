using ECommerce.Domain.Contracts.Specifications;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
   public abstract class BaseSpecifiications <TEntity,TKey> : ISpecifications<TEntity,TKey>
        where TEntity :BaseEntity<TKey>
    {
        protected BaseSpecifiications(Expression<Func<TEntity, bool>>_criteria)
        {
            criteria = _criteria;       
        }
        public Expression<Func<TEntity, bool>> criteria { get; private set; }
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> _OrderBy)
        {
            OrderBy = _OrderBy;
        } protected void AddOrderByDesc(Expression<Func<TEntity, object>> _OrderByDesc)
        {
            OrderByDesc = _OrderByDesc;
        }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>>IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }

    }
}
