using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Common
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int pageIndex, int pageSize, int totalCount, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
