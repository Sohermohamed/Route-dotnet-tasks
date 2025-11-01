using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.Seeds
{
   public interface IDataSeeding
    {
        Task DataSeed();
    }
}
