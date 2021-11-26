using Application.Common.Models;
using Application.Person.Queries.GetPersonAll;
using Identity.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Person.Test
{
    [TestClass]
     public  class GetPersonsWithPaginationTest : EnvironmentTest
    {
        private string Uri = "api/persons";

        [TestMethod]
        public async Task GetPersonsWithPagination()
        {
            // Initilization.  
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("PageNumber", "1"));
            allIputParams.Add(new KeyValuePair<string, string>("PageSize", "20"));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var response = await _client.GetAsync(Uri + "?" + requestParams);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedList<PersonDto>>(content);
        }
      }
}
