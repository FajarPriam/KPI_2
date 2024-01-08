using KPI_API.Views_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KPI_API.Controllers
{
    public class SettingDateController : ApiController
    {
        [HttpGet]
        [Route("getSettingDate")]
        public IHttpActionResult getSettingDate()
        {
            try
            {
                ClsSettingDate clsSettingDate = new ClsSettingDate();

                var data = clsSettingDate.c_getListSettingDate();

                return Ok(new { Status = true, Data = data });
            }
            catch (Exception e)
            {
                return Ok(new { Status = false, Message = "Error: " + e.Message.ToString() });
            }
        }
    }
}
