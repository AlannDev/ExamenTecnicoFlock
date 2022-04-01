using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamenTecnico.Models
{
    public class BaseResponse
    {
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
