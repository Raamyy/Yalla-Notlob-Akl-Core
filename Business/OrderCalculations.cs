using System.Collections.Generic;
using System.Linq;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Business
{
    public static class OrderCalculations
    {
        public static Dictionary<Item, int> GetOrderSummary(List<OrderItem> orderItems)
        {
            var itemsSummary =
            orderItems
            .GroupBy(orderItem => orderItem.ItemId)
            .Select(group =>
                            new
                            {
                                ItemId = group.Key,
                                ItemCount = group.Sum(i => i.Quantity)
                            });
            Dictionary<Item, int> dict = new Dictionary<Item, int>();
            foreach(var itemSummary in itemsSummary){
                dict[new ItemDao().Get(itemSummary.ItemId)] = itemSummary.ItemCount;
            }
            return dict;
        }
    }
    public class OrderStatsVM{
        public Dictionary<Item, int> OrderSummary {get; set;}
    }
}