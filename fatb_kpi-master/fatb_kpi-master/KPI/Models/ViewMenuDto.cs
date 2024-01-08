using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI.Models
{
    public class ViewMenuDto
    {
        public int? ID { get; set; }
        public int? ROLE_ID { get; set; }
        public string ROLE { get; set; }
        public int? MENU_ID { get; set; }
        public int? ID_PARENT_MENU { get; set; }
        public string MENU_DESC { get; set; }
        public string URL { get; set; }
        public string ICON { get; set; }
        public int? ORDER { get; set; }
    }
}