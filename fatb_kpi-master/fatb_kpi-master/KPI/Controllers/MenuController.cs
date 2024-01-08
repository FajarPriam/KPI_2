using KPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class MenuController : Controller
    {
        //DB_KPIDataContext db = new DB_KPIDataContext(ConfigurationManager.ConnectionStrings["DB_FATB_KPI_KPTConnectionString"].ConnectionString);
        private readonly HttpClient _httpClient = new HttpClient();

        /*public ActionResult Menu2()
        {

            if (Session["Nrp"] == null)
            {
                var menu = "";

                return PartialView("_PartialMenu", menu);
            }
            else
            {
                var menu = db.VW_MENU_ROLEs.Where(x => x.ROLE_ID == Convert.ToInt32(Session["IdProfile"].ToString())).OrderBy(x => x.ORDER).ToList();
                return PartialView("_PartialMenu", menu);

            }
        }*/
        public Task<ActionResult> Menu()
        {
            if (Session["Nrp"] == null)
            {
                var menu = "";

                return Task.FromResult<ActionResult>(PartialView("_PartialMenu", menu));
            }
            var idProfile = Session["IdProfile"];
            string url = ConfigurationManager.AppSettings["path"] + $"api/menu/GetMenu/{idProfile}";

            var responseBody = Task.Run(async () =>
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(jsonContent);
                    var data = jsonResponse.Data;
                    // Deserialize the "data" property into a collection of MenuModel objects.
                    IEnumerable<ViewMenuDto> menuItems = JsonConvert.DeserializeObject<IEnumerable<ViewMenuDto>>(data.ToString());

                    return menuItems;
                }

                return null;

            }).GetAwaiter().GetResult();
            return Task.FromResult<ActionResult>(PartialView("_PartialMenu", responseBody));
        }
    }
}