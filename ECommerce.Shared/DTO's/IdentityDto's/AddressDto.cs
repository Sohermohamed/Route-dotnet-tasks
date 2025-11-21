using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTO_s.IdentityDto_s
{
    public class AddressDto
    {
        public string Street { get; set; } = null!;
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
