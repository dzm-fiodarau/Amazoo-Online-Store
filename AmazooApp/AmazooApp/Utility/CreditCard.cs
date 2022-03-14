using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AmazooApp.Utility
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        public string name;
        public string cardNumber;
        public string expMonth;
        public string expYear;
        public string ccv;

        
        //Constructors
        public CreditCard()
        {
            name = "";
            cardNumber = "";
            expMonth = "";
            expYear = "";
            ccv = "";
        }

        public CreditCard(string n, string num, string mon, string year, string c)
        {
            name = c;
            cardNumber = num;
            expMonth = mon;
            expYear = year;
            ccv = c;
        }
    }


}
