
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AmazooApp.Utility
{
    public class MonthHelper
    { 

        public static List<SelectListItem> GetMonthsForDropDown()
        { 
                    return new List<SelectListItem>
            {
                new SelectListItem{Value="01" , Text="January"},
               new SelectListItem { Value = "02", Text = "Febuary" },
               new SelectListItem { Value = "03", Text = "March" },
               new SelectListItem { Value = "04", Text = "April" },
               new SelectListItem { Value = "05", Text = "May" },
               new SelectListItem { Value = "06", Text = "June" },
               new SelectListItem { Value = "07", Text = "July" },
               new SelectListItem { Value = "08", Text = "August" },
               new SelectListItem { Value = "09", Text = "September" },
               new SelectListItem { Value = "10", Text = "October" },
               new SelectListItem { Value = "11", Text = "November" },
               new SelectListItem { Value = "12", Text = "December" },

            };
        }

    
    }
}
