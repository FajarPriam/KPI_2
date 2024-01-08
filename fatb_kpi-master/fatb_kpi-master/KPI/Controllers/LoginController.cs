using KPI.Models;
using System.Configuration;
using System.Web.Mvc;

namespace KPI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session["path_api"] = ConfigurationManager.AppSettings["path"].ToString();

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public JsonResult setSession(VW_USER_PROFILE param)
        {
            string[] words = param.NAME.Split(' ');
            if (words.Length >= 2)
            {
                string firstChar = words[0].Substring(0, 1).ToUpper();
                string lastChar = words[words.Length - 1].Substring(0, 1).ToUpper();
                Session["UserInisial"] = firstChar + lastChar;
            }
            else
            {
                string firstChar = words[0].Substring(0, 1).ToUpper();
                Session["UserInisial"] = firstChar ;
            }

            Session["Nrp"] = param.USER;
            Session["Nama"] = param.NAME;
            Session["IdProfile"] = param.ID_ROLE;
            Session["Profile"] = param.ROLE;
            Session["District"] = param.DSTRCT_CODE;
            Session["PosId"] = param.POSITION_ID;
            Session["PosTitle"] = param.POS_TITLE;
            Session["DeptCode"] = param.DEPT_CODE;
            Session["DeptDesc"] = param.DEPT_DESC;

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}