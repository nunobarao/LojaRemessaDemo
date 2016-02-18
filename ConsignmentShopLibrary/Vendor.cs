using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    public class Vendor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Commission { get; set; }
        public decimal PaymentDue { get; set; }

        public string Display
        {
            get
            {
                //Retorna uma string onde o {0} - ${1} são placeholders, neste caso do Firstname, LastName,
                //e do PaymentDue.
                return string.Format("{0} {1} - {2}€", FirstName, LastName, PaymentDue.ToString("0.00"));
            }
        }

        public Vendor()
        {
            Commission = .5;  
        }
    }
}
