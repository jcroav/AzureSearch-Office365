using AzureSearchSPS.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureSearchSPS.Controllers
{
    public class SearchController : ApiController
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["apikey"];
        private static readonly string ServiceName = ConfigurationManager.AppSettings["servicename"];
        private static readonly string IndexName = ConfigurationManager.AppSettings["indexname"];

        // GET: api/Search/5
        public IEnumerable<Contacts> Get([FromUri]string value, [FromUri]string sort)
        {
            List<Contacts> contactsToReturn = new List<Contacts>();

            var parameters = new SearchParameters();

            List<string> orderby = new List<string>();
            orderby.Add(sort);
            List<string> highlight = new List<string>();
            highlight.Add("Name");
            highlight.Add("Department");
            highlight.Add("Job");

            parameters.OrderBy = orderby.AsReadOnly();
            parameters.HighlightFields = highlight.AsReadOnly();
            parameters.HighlightPreTag = "<strong>";
            parameters.HighlightPostTag = "</strong>";

            SearchServiceClient serviceClient = new SearchServiceClient(ServiceName, new SearchCredentials(ApiKey));
            SearchIndexClient indexClient = serviceClient.Indexes.GetClient(IndexName);
            DocumentSearchResult<Contacts> response = indexClient.Documents.Search<Contacts>(value, parameters);

            foreach (SearchResult<Contacts> result in response.Results)
            {
                string name = result.Document.Name, department = result.Document.Department, job = result.Document.Job; 

                if(result.Highlights != null)
                {
                    HitHighlights highlightResult = result.Highlights;

                    if (highlightResult.ContainsKey("Name"))
                        name = highlightResult["Name"][0];

                    if (highlightResult.ContainsKey("Department"))
                        department = highlightResult["Department"][0];

                    if (highlightResult.ContainsKey("Job"))
                        job = highlightResult["Job"][0];
                }
               

                Contacts contactToAdd = new Contacts{
                    ContactId = result.Document.ContactId,
                    Job = job,
                    Department = department,
                    Email = result.Document.Email,
                    Name = name
                };

                contactsToReturn.Add(contactToAdd);
            }

            return contactsToReturn;

        }
    }
}
