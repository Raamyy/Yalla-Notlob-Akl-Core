using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.DB
{
    public class ItemDao: LocalStorageDao<Item>
    {
        public ItemDao(): base("Items"){
            
        }
    }

    public class PersonDao: LocalStorageDao<Person>
    {
        public PersonDao(): base("Persons"){
            
        }
    }

    public class OrderItemDao: LocalStorageDao<OrderItem>
    {
        public OrderItemDao(): base("Order Items"){
            
        }
    }
}