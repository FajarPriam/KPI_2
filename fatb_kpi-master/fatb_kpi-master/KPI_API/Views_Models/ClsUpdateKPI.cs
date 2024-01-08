using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsUpdateKPI
    {
        public string CODE_SECTION { get; set; }
        public string KPI_CODE { get; set; }
        public string KPI_ITEM { get; set; }
        public string DESKRIPSI { get; set; }
        public string BOBOT { get; set; }
        public string TARGET { get; set; }
        public string YEAR { get; set; }
        public string EDIT_BY { get; set; }
    }
}
