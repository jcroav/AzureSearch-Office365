using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureSearchPopulationApplication
{
    class Program
    {
        // Set private static vars
        private static readonly string GraphUrl = ConfigurationManager.AppSettings["GraphUrl"];
        private static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"];
        private static readonly string Authority = ConfigurationManager.AppSettings["Authority"];
        private static readonly string Thumbprint = ConfigurationManager.AppSettings["Thumbprint"];
        private static readonly string ApiKey = ConfigurationManager.AppSettings["apikey"];
        private static readonly string ServiceName = ConfigurationManager.AppSettings["servicename"];

        static void Main(string[] args)
        {
            // STEP 1: Get the certificate
            var certificate = GetCertificate();

            // STEP 2: Get the access token
            var token = GetAccessToken(certificate);

            // STEP 3: Make Microsoft Graph Request
            var client = new RestClient(GraphUrl);
            var request = new RestRequest("/v1.0/users/[user-id]/contacts", Method.GET);
            request.AddHeader("Authorization", "Bearer" + token.Result);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);
            var content = response.Content;

            dynamic contacts = JObject.Parse(content);

            List<Contacts> contactsToPopulate = new List<Contacts>();

            int count = 1;

            foreach (var contact in contacts.value)
            {
                Contacts contactToAdd = new Contacts
                {
                    ContactId = count.ToString(),
                    Name = contact.displayName,
                    Department = contact.department,
                    Job = contact.jobTitle,
                    Email = contact.emailAddresses[0].address
                };

                count++;

                contactsToPopulate.Add(contactToAdd);
            }

            //populate


            // STEP 5: Populate Content

            SearchIndexClient indexClient = GetSearchIndexClient();

            var batch = IndexBatch.Upload(contactsToPopulate.ToArray());
            indexClient.Documents.Index(batch);


            Thread.Sleep(15000);
        }

        private static SearchIndexClient GetSearchIndexClient()
        {
            SearchIndexClient indexClient = new SearchIndexClient(ServiceName, "contacts", new SearchCredentials(ApiKey));

            return indexClient;
        }

        private static X509Certificate2 GetCertificate()
        {
            X509Certificate2 certificate = null;
            var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, Thumbprint, false);
            // Get the first cert with the thumbprint
            if (certCollection.Count > 0)
            {
                certificate = certCollection[0];
            }
            certStore.Close();
            return certificate;
        }

        private static async Task<string> GetAccessToken(X509Certificate2 certificate)
        {
            var authenticationContext = new AuthenticationContext(Authority, false);
            var cac = new ClientAssertionCertificate(ClientId, certificate);
            var authenticationResult = await authenticationContext.AcquireTokenAsync(GraphUrl, cac);
            return authenticationResult.AccessToken;
        }
    }
}
