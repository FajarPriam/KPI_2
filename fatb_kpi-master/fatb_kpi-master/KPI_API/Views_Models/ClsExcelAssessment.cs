using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsExcelAssessment
    {
        public ClsAssessment_DS[] clsAssessment_Ds { get; set; }
        public ClsAssessment_ON[] clsAssessment_On { get; set; }
    }
}