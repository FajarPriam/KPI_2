using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIAPI.Models.Dtos
{
    public class MappingKPIDSDto
    {
        public int ID { get; set; }

        public string KPI_DS_ID { get; set; }

        public string KPI_DS_DESC { get; set; }

        public string KPI_CODE { get; set; }

        public string KPI_ITEM { get; set; }

        public System.Nullable<decimal> BOBOT { get; set; }

        public System.Nullable<int> ID_PIC_OFFICER_NONOM { get; set; }

        public string INITIALS { get; set; }
        public IEnumerable<TBL_M_KPI> KPICodes { get; set; }

        public IEnumerable<TBL_M_PCI_OFFICER_NONOM> PICS { get; set; }
    }
    
    public class MappingKPIONDto
    {
        public int ID { get; set; }

        public string ID_KPI_ON { get; set; }

        public string KPI_DESC { get; set; }

        public string KPI_CODE { get; set; }

        public string KPI_ITEM { get; set; }

        public System.Nullable<decimal> BOBOT { get; set; }

        public System.Nullable<int> ID_PIC { get; set; }

        public string INITIALS { get; set; }
        public IEnumerable<cufn_GetKPIONMappingChildResult> KPICodes { get; set; }

        public IEnumerable<TBL_M_PCI_OFFICER_NONOM> PICS { get; set; }
    }
    
}