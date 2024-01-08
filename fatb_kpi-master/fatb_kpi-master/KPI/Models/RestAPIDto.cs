using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace KPI.Models
{
    public class RestAPIDto<T>
    {
        public bool? Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}