using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int StatusCode { get; set; } = (int) HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Error";
        public IEnumerable<ValidationError> ValidationErrors { get; set; } 
    }
}
