using Application.Person.Queries.GetPersonAll;
using Identity.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Person.Test
{
    [TestClass]
    public class GetAllTest : EnvironmentTest
    {
        private string Uri = "api/persons";

        [TestMethod]
        public async Task GetAllPersonsByName()
        {
            var query = new GetPersonAllQuery();
            query.Nombres = "Olson";
            query.Apellido = "";

            var body = JsonConvert.SerializeObject(query);

            // Initilization.  
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("Nombres", query.Nombres));
            allIputParams.Add(new KeyValuePair<string, string>("Apellido", query.Apellido));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var response = await _client.GetAsync(Uri + "/GetAll?" + requestParams);
            
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonDto>>(content);

        }

        [TestMethod]
        public async Task GetAllPersonsByLastName()
        {
            var query = new GetPersonAllQuery();
            query.Nombres = "";
            query.Apellido = "Aguilar";

            var body = JsonConvert.SerializeObject(query);

            // Initilization.  
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("Nombres", query.Nombres));
            allIputParams.Add(new KeyValuePair<string, string>("Apellido", query.Apellido));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var response = await _client.GetAsync(Uri + "/GetAll?" + requestParams);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonDto>>(content);

        }
        [TestMethod]
        public async Task GetAllPersonsByByNameAndLastName()
        {
            var query = new GetPersonAllQuery();
            query.Nombres = "Hampton";
            query.Apellido = "Tyler";

            var body = JsonConvert.SerializeObject(query);

            // Initilization.  
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("Nombres", query.Nombres));
            allIputParams.Add(new KeyValuePair<string, string>("Apellido", query.Apellido));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var response = await _client.GetAsync(Uri + "/GetAll?" + requestParams);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonDto>>(content);

        }

        [TestMethod]
        public async Task GetAllPersons()
        {
            var query = new GetPersonAllQuery();
            query.Nombres = "";
            query.Apellido = "";

            var body = JsonConvert.SerializeObject(query);

            // Initilization.  
            List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
            string requestParams = string.Empty;

            // Converting Request Params to Key Value Pair.  
            allIputParams.Add(new KeyValuePair<string, string>("Nombres", query.Nombres));
            allIputParams.Add(new KeyValuePair<string, string>("Apellido", query.Apellido));

            // URL Request Query parameters.  
            requestParams = new FormUrlEncodedContent(allIputParams).ReadAsStringAsync().Result;

            var response = await _client.GetAsync(Uri + "/GetAll?" + requestParams);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonDto>>(content);

        }
    }
}
