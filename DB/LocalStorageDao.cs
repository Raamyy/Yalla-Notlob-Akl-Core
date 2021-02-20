using Yalla_Notlob_Akl.Models;
using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yalla_Notlob_Akl.DB
{

    public class LocalStorageDao<T> : Dao<T> where T : BaseModel
    {
        public LocalStorageDao(string tableName)
        {
            this.tableName = tableName;
        }
        private string tableName;
        private static LocalStorage storage = new LocalStorage();

        public override T Create(T t)
        {
            var currentList = GetCurrentList();
            t.id = GenerateGUID();
            currentList.Add(t);
            storage.Store<List<T>>(tableName, currentList);
            storage.Persist();
            return t;
        }

        public override bool Delete(string id)
        {
            Console.WriteLine($"deleting {id}");
            var currentList = storage.Get<List<T>>(tableName);
            currentList.RemoveAll(i => i.id == id);
            storage.Store<List<T>>(tableName, currentList);
            storage.Persist();
            return true;
        }

        public override T Get(string id)
        {
            return storage.Get<List<T>>(tableName).Single(i => i.id == id);
        }

        public override bool Update(T t)
        {
            var currentList = storage.Get<List<T>>(tableName);
            for(int i = 0 ; i<currentList.Count; i++){
                if(currentList[i].id == t.id){
                    currentList[i] = t;
                    break;
                }
            }
            storage.Store<List<T>>(tableName, currentList);
            storage.Persist();
            return true;
        }

        private string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        private List<T> GetCurrentList(){
            if(storage.Exists(tableName)) return storage.Get<List<T>>(tableName);
            else return new List<T>();
        }

        public override List<T> GetAll()
        {
            return GetCurrentList();
        }
    }
}