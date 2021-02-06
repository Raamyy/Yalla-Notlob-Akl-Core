using System.Collections.Generic;

namespace Yalla_Notlob_Akl.Models
{
    public class OrderItem : BaseModel
    {
        public string ItemId { get; set; }
        public string PersonId { get; set; }
        public int Quantity { get; set; }

        public OrderItem(): base("OrderItem"){
            ItemId = "";
            PersonId = "";
            Quantity = 0;
        }
        public OrderItem(string itemId, string personId, int quantity) : base("OrderItem")
        {
            ItemId = itemId;
            PersonId = personId;
            Quantity = quantity;
        }
    }

    public class OrderItemViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<Item> Items { get; set; }

        public List<Person> Persons { get; set; }

    }
}