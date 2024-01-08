using System;
using System.Configuration;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class HomeController : Controller
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

    }
}