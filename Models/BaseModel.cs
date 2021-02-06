
using Hanssens.Net;

namespace Yalla_Notlob_Akl.Models
{
    public abstract class BaseModel
    {
        public string id { get; set; }

        public string ModelType { get; set; }

        public BaseModel(string ModelType)
        {
            this.ModelType = ModelType;
        }
    }
}
