using Application.Person.Queries.GetPersonAll;
using Identity.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Test
{
    [TestClass]
   public class GetByIdTest : EnvironmentTest
    {
        private string Uri = "api/persons";

        [TestMethod]
        public async Task GetById()
        {
            var response = await _client.GetAsync(Uri + "/1" );

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonDto>(content);
        }
    }
}
