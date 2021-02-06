using System.Collections.Generic;
using Hanssens.Net;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.DB {
    public abstract class Dao<T>{
        public abstract T Get(string id);
        public abstract List<T> GetAll();
        public abstract T Create(T t);
        public abstract bool Update(T t);
        public abstract bool Delete(string id);
    }
}
