using AGSports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AGSports_tests
{
    [TestClass]
   public class CustomerIntegrationTest
    {
        private readonly HttpClient _client;

        public CustomerIntegrationTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [TestMethod]

        public void CustomerGetAllTest()

        {
            //arrange

            var request = new HttpRequestMessage(new HttpMethod("GEt"),"/api/Customers");

            //act

            var response = _client.SendAsync(request).Result;

            //assert


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow(100)]
        public void CustomerGetOneTest(int id)

        {
            //arrange

            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/Customers/{id}");

            //act

            var response = _client.SendAsync(request).Result;

            //assert


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
