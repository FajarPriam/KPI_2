using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsMenu
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        public List<VW_MENU_ROLE> FindMenuByProfile(int id)
        {
            var menus = db.VW_MENU_ROLEs.Where(x => x.ROLE_ID == id ).OrderBy(x => x.ORDER).ToList();
            return menus;
        }
    }
}