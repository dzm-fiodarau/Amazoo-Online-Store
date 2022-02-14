using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AmazooApp.Utility
{
    public class RoleHelper
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";

        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Customer , Text=RoleHelper.Customer},
                new SelectListItem{Value = Admin , Text=RoleHelper.Admin}
            };
        }
    }
}
