using System.Collections.Generic;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.ViewModels {
    public class OrderItemViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<Item> Items { get; set; }

        public List<Person> Persons { get; set; }

    }
}