using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTO_s.IdentityDto_s
{
    public class UserDto
    {
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
