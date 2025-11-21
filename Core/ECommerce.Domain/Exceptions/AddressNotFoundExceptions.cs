using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public class AddressNotFoundExceptions(string Name): NotFoundException($"User With Name {Name} Has no Address")
    {

    }
}
