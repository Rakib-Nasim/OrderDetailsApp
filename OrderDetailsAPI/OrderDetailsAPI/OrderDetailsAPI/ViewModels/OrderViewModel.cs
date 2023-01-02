using OrderDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetailsAPI.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public string OrderAdderss { get; set; }
        public string OrderDate { get; set; }
        public double TotalPrice { get; set; }

        public int? CustomerId { get; set; }

        public  List<int> Order_Items { get; set; }
    }
}
