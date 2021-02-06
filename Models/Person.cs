namespace Yalla_Notlob_Akl.Models
{
        public class Person : BaseModel
    {
       public string name {get;set;}
        
        public Person(): base("Person"){
            this.name = "";
        }
        public Person(string name): base("Person"){
            this.name = name;
        }
    }
}