using Yalla_Notlob_Akl.Models;
using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Yalla_Notlob_Akl.DB
{

    public class LocalStorageDao<T> : Dao<T> where T : BaseModel
    {
        public LocalStorageDao(string tableName)
        {
            this.tableName = tableName;
            HttpContextAccessor _accessor = new HttpContextAccessor();
            _sessionId = _accessor.HttpContext.Session.Id;
            Console.WriteLine(_sessionId);
            InitLocalStorage();
        }
        private string tableName;

        // sessionId mapped to it's localstorage
        private static Dictionary<string, LocalStorage> storage = new Dictionary<string, LocalStorage>();

        private string _sessionId;
        public override T Create(T t)
        {
            var currentList = GetCurrentList();
            t.id = GenerateGUID();
            currentList.Add(t);
            storage[_sessionId].Store<List<T>>(tableName, currentList);
            storage[_sessionId].Persist();
            return t;
        }

        public override bool Delete(string id)
        {
            Console.WriteLine($"deleting {id}");
            var currentList = storage[_sessionId].Get<List<T>>(tableName);
            currentList.RemoveAll(i => i.id == id);
            storage[_sessionId].Store<List<T>>(tableName, currentList);
            storage[_sessionId].Persist();
            return true;
        }

        public override T Get(string id)
        {
            return storage[_sessionId].Get<List<T>>(tableName).Single(i => i.id == id);
        }

        public override bool Update(T t)
        {
            var currentList = storage[_sessionId].Get<List<T>>(tableName);
            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].id == t.id)
                {
                    currentList[i] = t;
                    break;
                }
            }
            storage[_sessionId].Store<List<T>>(tableName, currentList);
            storage[_sessionId].Persist();
            return true;
        }

        private string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        private List<T> GetCurrentList()
        {
            if (storage[_sessionId].Exists(tableName)) return storage[_sessionId].Get<List<T>>(tableName);
            else return new List<T>();
        }

        public override List<T> GetAll()
        {
            return GetCurrentList();
        }

        private void InitLocalStorage()
        {
            if (!storage.ContainsKey(_sessionId)) storage[_sessionId] = new LocalStorage(
                new LocalStorageConfiguration
                {
                    Filename = $"DATA_{_sessionId}",
                    AutoLoad = true
                }
            );
        }
    }
}