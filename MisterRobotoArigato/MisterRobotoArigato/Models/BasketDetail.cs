﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisterRobotoArigato.Models
{
    public class BasketDetail
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string CustomerID { get; set; } //this will be tied to user email
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
