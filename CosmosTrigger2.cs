using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class CosmosTrigger2
    {
        [FunctionName("CosmosTrigger2")]
        public static void Run([CosmosDBTrigger(
            databaseName: "mydb",
            collectionName: "vehi",
            ConnectionStringSetting = "cosmosconnectionstring",
            LeaseCollectionName = "leases",CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                var vehicleno=input[0].GetPropertyValue<string>("vehicleno");
                var speed=input[0].GetPropertyValue<double>("speed");
                var city=input[0].GetPropertyValue<string>("city");
                var mobile=input[0].GetPropertyValue<string>("mobile"); 
                if(speed>80)
                {
                    var message=$"High speed {speed} detected in {city} with vehicle no {vehicleno} and mobileno {mobile}";
                    log.LogWarning(message);
                }
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
