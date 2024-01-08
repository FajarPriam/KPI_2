using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class SettingDateController : Controller
    {
        // GET: SettingDate
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
    }
}