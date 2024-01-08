using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsExcelKPI
    {
        public ClsTemplate[] clsTemplates { get; set; }
        public ClsTemplateCash[] clsTemplatesCash { get; set; }
        public ClsTemplateDS[] clsTemplateDs { get; set; }
    }
}