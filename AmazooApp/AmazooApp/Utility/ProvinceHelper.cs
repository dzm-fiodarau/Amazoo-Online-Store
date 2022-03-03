
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace AmazooApp.Utility

{
    public class ProvinceHelper
    { 

        public static List<SelectListItem> GetProvincesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value="AB" , Text="Alberta"},
               new SelectListItem{Value="BC" , Text="British Columbia"},
               new SelectListItem{Value="MB" , Text="Manitoba"},
               new SelectListItem{Value="NB" , Text="New Brunswick"},
               new SelectListItem{Value="NL" , Text="Newfoundland and Labrador"},
               new SelectListItem{Value="NS" , Text="Nova Scotia"},
               new SelectListItem{Value="ON" , Text="Ontario"},
               new SelectListItem{Value="P.E.I" ,Text="Prince Edward Island"},
               new SelectListItem{Value="QC" , Text="Quebec"},
               new SelectListItem{Value="SK" , Text="Saskatchewan"},
               new SelectListItem{Value="YK" , Text="Yukon"},
               new SelectListItem{Value="NT" , Text="Northwest Territories"},

            };
        }

    }
}
