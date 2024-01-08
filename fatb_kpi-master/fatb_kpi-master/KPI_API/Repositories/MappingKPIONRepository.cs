using KPI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KPI_API.Repositories
{
    public class MappingKPIONRepository
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);
        public IEnumerable<TBL_M_MAPPING_KPI_ON> GetsMappingKPIByKPION(string kpiON)
        {

            var results = db.TBL_M_MAPPING_KPI_ONs.Where(f => f.ID_KPI_ON.ToUpper() == kpiON.ToUpper());
            return results;
        }

        public bool AddMappingKPI(TBL_M_MAPPING_KPI_ON data)
        {
            if (data == null) return true;

            db.TBL_M_MAPPING_KPI_ONs.InsertOnSubmit(data);
            db.SubmitChanges();
            return true;
        }

        public bool UpdateMappingKPI(TBL_M_MAPPING_KPI_ON updateData)
        {
            var data = db.TBL_M_MAPPING_KPI_ONs.FirstOrDefault(f => f.ID == updateData.ID);
            if (data == null) return true;

            data.ID_KPI_ON = updateData.ID_KPI_ON;
            data.KPI_CODE = updateData.KPI_CODE;
            data.ID_PIC = updateData.ID_PIC;

            db.SubmitChanges();
            return true;
        }

        public bool DeleteMappingKPI(int id)
        {
            var deleteMapping = db.TBL_M_MAPPING_KPI_ONs.Where(x => x.ID == id).FirstOrDefault();

            if (deleteMapping == null) return true;

            db.TBL_M_MAPPING_KPI_ONs.DeleteOnSubmit(deleteMapping);
            db.SubmitChanges();
            return true;
        }
    }
}