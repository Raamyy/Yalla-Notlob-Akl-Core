using System.Collections.Generic;
using System.Linq;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Business
{
    public class PersonOrderSummary{
        public double totalPrice;
        public double basePrice;
        public double taxFees;
        public double deleivryFees;
        public List<OrderItem> orderItems;
        public PersonOrderSummary(){
            totalPrice = basePrice = taxFees = deleivryFees = 0;
            orderItems = new List<OrderItem>();
        }
    }
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
        public static Dictionary<Person, PersonOrderSummary> GetOrderReceipt(List<OrderItem> orderItems, double taxFees, double deleivryFees){
            double basePrice = 0;
            Dictionary<Person, PersonOrderSummary> personOrder = new Dictionary<Person, PersonOrderSummary>();
            foreach (var orderItem in orderItems)
            {
                Item item = new ItemDao().Get(orderItem.ItemId);
                double currentItemPrice = item.price.Value * orderItem.Quantity;
                basePrice += currentItemPrice;
                Person person = new PersonDao().Get(orderItem.PersonId);
                personOrder[person].basePrice += currentItemPrice;
                personOrder[person].orderItems.Add(orderItem);
            }
            foreach (var item in personOrder)
            {
                item.Value.taxFees = (item.Value.basePrice / basePrice) * taxFees;
                item.Value.deleivryFees = (item.Value.basePrice / basePrice) * deleivryFees;
                item.Value.totalPrice = item.Value.basePrice + item.Value.taxFees + item.Value.deleivryFees;
            }
            return personOrder;
        }
    }
    public class OrderStatsVM{
        public Dictionary<Item, int> OrderSummary {get; set;}
    }
}