using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.ErrorModels
{
    public class ErrorToReturn
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public List<string>? Errors { get; set; }
    }
}
