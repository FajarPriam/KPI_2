using KPI_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KPI_API.Controllers
{
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        [Route("GetMenu/{id}")]
        [HttpGet]
        public IHttpActionResult GetMenu(int id)
        {
            try
            {
                MenuService menuService = new MenuService();
                var menus = menuService.GetMenuByProfile(id);
                return Content(HttpStatusCode.OK, new { Status = HttpStatusCode.OK, Data = menus, Message = "Success to Get user" });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Status = HttpStatusCode.BadRequest, Data = "", Message = "Failed to Get Menu" });
            }
        }
    }
}
