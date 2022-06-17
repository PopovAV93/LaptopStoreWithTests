using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public async Task CheckStatus_Index_ShouldReturnOk()
        {
            //Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();
            //Act
            HttpResponseMessage response = await httpClient.GetAsync("Home/Index");
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
