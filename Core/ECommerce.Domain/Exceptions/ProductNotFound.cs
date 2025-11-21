using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public sealed class ProductNotFound(int id) : NotFoundException($"Product with {id} is Not Found")
    {

    }
}
