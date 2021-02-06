namespace Yalla_Notlob_Akl.Models
{
    public class Item : BaseModel
    {
        public string name{ get; set; }
        public double? price{ get; set; }


        public Item(): base("Item")
        {
            this.name = "";
        }        
        public Item(string name, double? price) : base("Item")
        {
            this.name = name;
            this.price = price;
        }
    }
}