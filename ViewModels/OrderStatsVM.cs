using System.Collections.Generic;
using Yalla_Notlob_Akl.Models;
using Yalla_Notlob_Akl.Business;
namespace Yalla_Notlob_Akl.ViewModels
{
    public class OrderStatsVM
    {
        public Dictionary<Item, int> OrderSummary { get; set; }
        public Dictionary<Person, PersonOrderSummary> OrderReciept { get; set; }
        public double OrderBasePrice { get; set; }
        public double OrderTaxCost { get; set; }
        public double OrderDeliveryCost { get; set; }
        public double OrderTotalPrice { get; set; }
    }
}