using KPI_API.Models;
using KPI_API.Views_Models;
using System;
using System.Web.Http;

namespace KPI_API.Controllers
{
    public class RoleController : ApiController
    {
        [HttpPost]
        [Route("CreateOrUpdateRole")]
        public IHttpActionResult CreateOrUpdateRole(TBL_M_ROLE param)
        {
            try
            {
                ClsRole clsRole = new ClsRole();
                clsRole.c_InsertOrUpdate(param);

                return Ok(new { Status = true, Message = "Insert Role Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteRole/{id}")]
        public IHttpActionResult DeleteRole(int id)
        {
            try
            {
                ClsRole clsRole = new ClsRole();
                clsRole.c_deleteRole(id);

                return Ok(new { Status = true, Message = "Delete Role Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }
    }
}
