using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;


namespace Sergel.DeliveryReport.DataAccess
{
    public interface ICosmosDbService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id);

        //uncomment below code as per future requiremnt.

        //Task AddItemAsync(T item,string key);
        Task UpdateItemAsync(string id, T item);
        //Task DeleteItemAsync(string id);
    }
}
