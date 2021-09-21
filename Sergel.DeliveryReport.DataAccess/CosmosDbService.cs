using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess
{
    public abstract class CosmosDbService<TObject> : ICosmosDbService<TObject> 
    {
        private Container _container;
        public CosmosDbService(
            DbConfig dbConfig)
        {
            this._container = dbConfig.DbClient.GetContainer(dbConfig.DatabaseName, dbConfig.ContainerName);
        }

        //uncomment below code as per future requirement.

        //public async Task AddItemAsync(TObject item, string key)
        //{
        //    try
        //    {
        //        await this._container.CreateItemAsync<TObject>(item, new PartitionKey(key));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task DeleteItemAsync(string id)
        //{
        //    try
        //    {
        //        await this._container.DeleteItemAsync<TObject>(id, new PartitionKey(id));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
            
        //}

        public async Task<TObject> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<TObject> response = await this._container.ReadItemAsync<TObject>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TObject>> GetItemsAsync(string query)
        {
            try
            {
                var result = this._container.GetItemQueryIterator<TObject>(new QueryDefinition(query));
                List<TObject> results = new List<TObject>();
                while (result.HasMoreResults)
                {
                    var response = await result.ReadNextAsync();

                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task UpdateItemAsync(string id, TObject item)
        {
            try
            {
                await this._container.UpsertItemAsync<TObject>(item, new PartitionKey(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
