using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Seed
{
    public interface IDataSeeding
    {
        Task DataSeedAsync();

        Task IdentitySeedAsync();
    }
}
