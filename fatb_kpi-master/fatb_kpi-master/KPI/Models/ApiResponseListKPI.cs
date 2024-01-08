using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI.Models
{
    public class ApiResponseListKPI
    {
        public bool Status { get; set; }
        [JsonProperty("Data")]
        public IList<VW_ITEM_KPI> Data { get; set; }
    }
}