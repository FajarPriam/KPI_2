using KPI_API.Models;
using System.Configuration;
using System.Linq;

namespace KPI_API.Views_Models
{
    public class ClsRole
    {
        DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);

        public void c_deleteRole(int id)
        {
            var data = db.TBL_M_ROLEs.Where(a => a.ID == id).FirstOrDefault();
            db.TBL_M_ROLEs.DeleteOnSubmit(data);
            db.SubmitChanges();
        }

        public void c_InsertOrUpdate(TBL_M_ROLE param)
        {
            var data = db.TBL_M_ROLEs.Where(a => a.ID == param.ID).FirstOrDefault();
            if (data != null)
            {
                data.ROLE = param.ROLE;
            }
            else
            {
                TBL_M_ROLE user = new TBL_M_ROLE();
                user.ID = param.ID;
                user.ROLE = param.ROLE;

                db.TBL_M_ROLEs.InsertOnSubmit(user);
            }
            db.SubmitChanges();
        }
    }
}