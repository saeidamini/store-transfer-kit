using System.Collections.Generic;
using RestSharp;
using StoreTransferKit.Models;

namespace StoreTransferKit.Callers
{
    public interface ICaller<T> where T : Factor
    {
        List<T> GetItems();
        IRestResponse Create(T item);
        void Update(int id, T item);
        void Delete(int id);
    }
}