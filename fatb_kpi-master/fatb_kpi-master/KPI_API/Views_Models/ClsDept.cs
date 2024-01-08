using KPI_API.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KPI_API.Views_Models
{
    public class ClsDept
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        public IQueryable<TBL_M_DEPT_IN_CHARGE> c_getAllDept()
        {
            var data = db.TBL_M_DEPT_IN_CHARGEs.ToList().AsQueryable();
            return data;
        }

        public IEnumerable<VW_DEPARTMENT_ELP> c_getDeptELP()
        {
            var data = db.VW_DEPARTMENT_ELPs.ToList();
            return data;
        }

        public void c_deleteDept(string id)
        {
            var data = db.TBL_M_DEPT_IN_CHARGEs.Where(a => a.ID == id).FirstOrDefault();
            db.TBL_M_DEPT_IN_CHARGEs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_InsertOrUpdate(TBL_M_DEPT_IN_CHARGE param)
        {
            var data = db.TBL_M_DEPT_IN_CHARGEs.Where(a => a.ID == param.ID).FirstOrDefault();
            if (data != null)
            {
                data.DEPT_CODE = param.DEPT_CODE;
                data.DEPT = param.DEPT;
            }
            else
            {
                TBL_M_DEPT_IN_CHARGE user = new TBL_M_DEPT_IN_CHARGE();
                user.ID = param.ID;
                user.DEPT = param.DEPT;
                user.DEPT_CODE = param.DEPT_CODE;

                db.TBL_M_DEPT_IN_CHARGEs.InsertOnSubmit(user);
            }
            db.SubmitChanges();
        }
    }
}