using System;
using System.Web.Http;
using KPI_API.Models;
using KPI_API.Views_Models;

namespace KPI_API.Controllers
{
    public class DeptController : ApiController
    {
        [HttpPost]
        [Route("CreateOrUpdateDept")]
        public IHttpActionResult CreateOrUpdateDept(TBL_M_DEPT_IN_CHARGE param)
        {
            try
            {
                ClsDept clsDept = new ClsDept();
                clsDept.c_InsertOrUpdate(param);

                return Ok(new { Status = true, Message = "Insert Dept Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("DeleteDept/{id}")]
        public IHttpActionResult DeleteDept(string id)
        {
            try
            {
                ClsDept clsDept = new ClsDept();
                clsDept.c_deleteDept(id);

                return Ok(new { Status = true, Message = "Delete Dept Success" });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = e.Message.ToString() });
            }
        }


        [HttpGet]
        [Route("getAllDept")]
        public IHttpActionResult getAllDept()
        {
            try
            {
                ClsDept clsDept = new ClsDept();

                var list = clsDept.c_getAllDept();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("getDeptELP")]
        public IHttpActionResult getDeptELP()
        {
            try
            {
                ClsDept clsDept = new ClsDept();

                var list = clsDept.c_getDeptELP();

                return Ok(new { Status = true, data = list });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }

    }
}
