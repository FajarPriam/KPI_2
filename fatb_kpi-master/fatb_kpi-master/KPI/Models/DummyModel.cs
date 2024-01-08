using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI.Models
{
    public class DummyModel
    {
    
    }
    public class VW_USER_PROFILE
    {
        public int? ID { get; set; }
        public int? ID_ROLE { get; set; }
        public string ROLE { get; set; }
        public string USER { get; set; }
        public string NAME { get; set; }
        public string POSITION_ID { get; set; }
        public string POS_TITLE { get; set; }
        public string DSTRCT_CODE { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_DESC { get; set; }
    }
    public partial class VW_ITEM_KPI
    {

        public System.Nullable<int>  ID_ON { get; set; }

        public string ITEM_ON { get; set; }

        public System.Nullable<int> ID_KPI_DS { get; set; }

        public string ITEM_DS { get; set; }

        public System.Nullable<int> ID_KPI_O { get; set; }

        public string ITEM_O { get; set; }

        public string ID_DEPT { get; set; }

        public string DEPT { get; set; }

        public string DEPT_CODE { get; set; }

        public System.Nullable<int> ID_POS_EVALUATE { get; set; }

        public string POSITION { get; set; }

        public System.Nullable<int> PERIODE { get; set; }

        public System.Nullable<int> YEAR { get; set; }

        public System.Nullable<int> BOBOT { get; set; }
    }
}