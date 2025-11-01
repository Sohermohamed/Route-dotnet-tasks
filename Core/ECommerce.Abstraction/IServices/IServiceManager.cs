using ECommerce.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
   public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
