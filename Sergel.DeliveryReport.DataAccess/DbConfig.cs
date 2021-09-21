using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess
{
    public class DbConfig
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public CosmosClient DbClient { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
    }
}
