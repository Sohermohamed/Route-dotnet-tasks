using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IServicesManger
    {
        public IProductServices ProductServices { get; }

        public IBasketServices BasketServices { get; }

        public IAuthenticationServices AuthenticationServices { get; }
    }
}
