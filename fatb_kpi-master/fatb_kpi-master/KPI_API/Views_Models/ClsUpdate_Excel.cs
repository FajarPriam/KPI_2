using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsUpdate_Excel
    {
        public ClsUpdateKPI[] clsUpdateKPI { get; set; }
        public ClsUpdateCashier[] clsUpdateCashier { get; set; }
        public ClsUpdateDS[] clsUpdateDS { get; set; }
    }
}