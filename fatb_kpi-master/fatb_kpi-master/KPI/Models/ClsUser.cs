using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI.Models
{
    public class ClsUser
    {
        public string NRP { get; set; }
        public string NAME { get; set; }
        public int ID_ROLE { get; set; }
        public string ROLE { get; set; }
        public string DISTRICT { get; set; }
        public string POSITION_ID { get; set; }
        public string POS_TITLE { get; set; }
    }
}