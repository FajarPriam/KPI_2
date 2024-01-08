using KPI_API.Models;
using KPI_API.Views_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPI_API.Services
{
    public class MenuService
    {
        public List<VW_MENU_ROLE> GetMenuByProfile(int role)
        {
            try
            {
                ClsMenu clsMenu = new ClsMenu();
                var menus = clsMenu.FindMenuByProfile(role);

                return menus;
            }
            catch (Exception ex)
            {
                //new ClsLogError().InsertLogError(ex.Message, ex.StackTrace);
                return new List<VW_MENU_ROLE>();
            }
        }
    }
}