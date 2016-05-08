using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchIndexApplication
{
    class Program
    {
        //Set Private Static Vars SearchService Apikey
        private static readonly string ApiKey = ConfigurationManager.AppSettings["apikey"];
        private static readonly string ServiceName = ConfigurationManager.AppSettings["servicename"];

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Creating Index");

                Console.Write("Write the name of the index=>");
                string indexName = Console.ReadLine();

                //STEP 1: Connect to Azure Search
                SearchServiceClient serviceClient = ConnectService(ApiKey, ServiceName);

                //STEP 2: Create the index
                Index indexToAdd = CreateIndex(indexName);

                //STEP 3: Populate index in Azure Search
                PopulateIndex(serviceClient, indexToAdd);


                Console.WriteLine("Index created succesfully!!!");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong!!!");
                Console.WriteLine(ex.Message);
                Console.Read();
            }

        }

        private static void PopulateIndex(SearchServiceClient serviceClient, Index indexToAdd)
        {
            //To do create index
            DeleteIndexIfExists(serviceClient, indexToAdd.Name);
            serviceClient.Indexes.Create(indexToAdd);
        }

        private static void DeleteIndexIfExists(SearchServiceClient serviceClient, string indexName)
        {
            //To do delete index
            if (serviceClient.Indexes.Exists(indexName))
                serviceClient.Indexes.Delete(indexName);
        }

        private static Index CreateIndex(string indexName)
        {
            //To do create index
            Index indexToAdd = new Index
            {
                Name = indexName,
                Fields = new[]{
                    new Field("ContactId", DataType.String) {IsKey = true,  IsRetrievable = true, IsFilterable = true},
                    new Field("Name",DataType.String) { IsRetrievable = true, IsSearchable=true, IsSortable=true, IsFilterable = true},
                    new Field("Job", DataType.String) { IsRetrievable = true, IsSearchable = true, IsSortable=true, IsFilterable = true},
                    new Field("Department", DataType.String) { IsRetrievable = true, IsSearchable = true, IsSortable=true, IsFilterable = true},
                    new Field("Email", DataType.String) { IsRetrievable = true, IsSearchable = true, IsSortable=true, IsFilterable = true}
                }
            };

            return indexToAdd;
        }

        private static SearchServiceClient ConnectService(string ApiKey, string ServiceName)
        {
            //To do service client
            SearchServiceClient serviceClient = new SearchServiceClient(ServiceName, new SearchCredentials(ApiKey));
            return serviceClient;
        }
    }
}
