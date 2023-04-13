using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCOMMERCEApplication.Models
{
    public class Item
    {
        public tblProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}