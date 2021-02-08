using System;
using System.Collections.Generic;
using System.Linq;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Business
{
    public class PersonOrderSummary
    {
        public double totalPrice;
        public double basePrice;
        public double taxFees;
        public double deleivryFees;
        public List<OrderItem> orderItems;

        public bool unKnownPrice;
        public PersonOrderSummary()
        {
            totalPrice = basePrice = taxFees = deleivryFees = 0;
            orderItems = new List<OrderItem>();
            unKnownPrice = true;
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
            foreach (var itemSummary in itemsSummary)
            {
                dict[new ItemDao().Get(itemSummary.ItemId)] = itemSummary.ItemCount;
            }
            return dict;
        }
        public static Dictionary<Person, PersonOrderSummary> GetOrderReceipt(List<OrderItem> orderItems, double taxFees, double deleivryFees)
        {
            double basePrice = 0;
            Dictionary<string, PersonOrderSummary> personOrder = new Dictionary<string, PersonOrderSummary>();
            foreach (var orderItem in orderItems)
            {
                Item item = new ItemDao().Get(orderItem.ItemId);
                if (!personOrder.ContainsKey(orderItem.PersonId)) personOrder[orderItem.PersonId] = new PersonOrderSummary();
                if (item.price.HasValue)
                {
                    double currentItemPrice = item.price.Value * orderItem.Quantity;
                    basePrice += currentItemPrice;                    
                    personOrder[orderItem.PersonId].basePrice += currentItemPrice;
                    personOrder[orderItem.PersonId].orderItems.Add(orderItem);
                    personOrder[orderItem.PersonId].unKnownPrice = false;
                }else{
                    personOrder[orderItem.PersonId].unKnownPrice = true;
                }
            }
            foreach (var item in personOrder)
            {
                item.Value.taxFees = Math.Round((item.Value.basePrice / basePrice) * taxFees,2);
                item.Value.deleivryFees = Math.Round((item.Value.basePrice / basePrice) * deleivryFees, 2);
                item.Value.totalPrice = item.Value.basePrice + item.Value.taxFees + item.Value.deleivryFees;
            }
            Dictionary<Person, PersonOrderSummary> dict = new Dictionary<Person, PersonOrderSummary>();
            foreach (var receipt in personOrder)
            {
                dict[new PersonDao().Get(receipt.Key)] = receipt.Value;
            }
            return dict;
        }
    }
    public class OrderStatsVM
    {
        public Dictionary<Item, int> OrderSummary { get; set; }
        public Dictionary<Person, PersonOrderSummary> OrderReciept { get; set; }

    }
}