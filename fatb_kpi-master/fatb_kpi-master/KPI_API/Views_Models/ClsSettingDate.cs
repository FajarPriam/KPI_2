using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KPI_API.Views_Models
{
    public class ClsSettingDate
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        public IEnumerable<TBL_R_SETTING_DATE> c_getListSettingDate()
        {
            var data = db.TBL_R_SETTING_DATEs.ToList();
            return data;
        }
    }
}