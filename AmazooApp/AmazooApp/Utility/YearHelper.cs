
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace AmazooApp.Utility
{
    public class YearHelper
    {

        public static List<SelectListItem> GetYearsForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value="2022" , Text="2022"},
               new SelectListItem { Value = "2023", Text = "2023" },
               new SelectListItem { Value = "2024", Text = "2024" },
               new SelectListItem { Value = "2025", Text = "2025" },
               new SelectListItem { Value = "2026", Text = "2026" },
               new SelectListItem { Value = "2027", Text = "2027" },
               new SelectListItem { Value = "2028", Text = "2028" },

            };
        }
    }
}
