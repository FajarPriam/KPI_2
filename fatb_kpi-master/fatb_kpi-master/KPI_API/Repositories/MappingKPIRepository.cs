using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KPI_API.Repositories
{
    public class MappingKPIRepository
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);
        public IEnumerable<TBL_M_MAPPING_KPI> GetsMappingKPIByKPIDS(string kpiDS)
        {

            var results = db.TBL_M_MAPPING_KPIs.Where(f => f.ID_KPI_DS.ToUpper() == kpiDS.ToUpper());
            return results;
        }

        public bool AddMappingKPI(TBL_M_MAPPING_KPI data)
        {
            if (data == null) return true;

            db.TBL_M_MAPPING_KPIs.InsertOnSubmit(data);
            db.SubmitChanges();
            return true;
        }
        
        public bool UpdateMappingKPI(TBL_M_MAPPING_KPI updateData)
        {
            var data = db.TBL_M_MAPPING_KPIs.FirstOrDefault(f => f.ID == updateData.ID);
            if (data == null) return true;

            data.ID_KPI_DS = updateData.ID_KPI_DS;
            data.KPI_CODE = updateData.KPI_CODE;
            data.ID_PIC_OFFICER_NONOM = updateData.ID_PIC_OFFICER_NONOM;

            db.SubmitChanges();
            return true;
        }

        public bool DeleteMappingKPI(int id)
        {
            var deleteMapping = db.TBL_M_MAPPING_KPIs.Where(x => x.ID == id).FirstOrDefault();

            if (deleteMapping == null) return true;

            db.TBL_M_MAPPING_KPIs.DeleteOnSubmit(deleteMapping);
            db.SubmitChanges();
            return true;
        }
    }
}