using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class MasterController : Controller
    {
        #region Users
        public ActionResult Users()
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
        #endregion

        #region Role
        public ActionResult Role()
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
        #endregion


        #region Dept
        public ActionResult Dept()
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
        #endregion

        #region Kpi
        public ActionResult KPI()
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
        #endregion

        #region MappingKpi
        public ActionResult MappingKPI()
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
        #endregion


    }
}