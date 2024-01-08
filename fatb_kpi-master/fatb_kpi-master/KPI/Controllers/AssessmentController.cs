using KPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class AssessmentController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["Nrp"] as string))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login"); ;
            }
        }

        //private readonly HttpClient _httpClient;

        //public AssessmentController()
        //{
        //    _httpClient = new HttpClient();
        //    _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["path"].ToString());
        //}

        //public async Task<ActionResult> Index()
        //{
        //    if (!string.IsNullOrEmpty(Session["Nrp"] as string))
        //    {
        //        ApiResponseListKPI apiResponse = new ApiResponseListKPI();
        //        DateTime dateTime = DateTime.Now;
        //        int periode = dateTime.Month;
        //        int year = dateTime.Year;
        //        string dept = Session["DeptCode"].ToString();

        //        string pathAPI = $"GetItemKPIDept/{dept}/{year}/{periode}";
        //        if (Session["Profile"].ToString() == "Admin")
        //        {
        //            pathAPI = $"GetItemKPIPeriode/{periode}/{year}"; ;
        //        }

        //        HttpResponseMessage response = await _httpClient.GetAsync(pathAPI);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonContent = await response.Content.ReadAsStringAsync();
        //            apiResponse = JsonConvert.DeserializeObject<ApiResponseListKPI>(jsonContent);
        //        }

        //        return View(apiResponse.Data);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //}


    }
}