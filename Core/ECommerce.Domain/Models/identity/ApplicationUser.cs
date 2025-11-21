using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;

        public Address Address { get; set; }
    }
}
