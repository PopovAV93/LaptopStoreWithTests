using LaptopStore.Data;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using LaptopStore.Data.Models;
using LaptopStore.Data.Repository;
using LaptopStore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class OrderControllerTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task Checkout_ShouldReturnModelStateError()
        {
            //Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Cart cart = new Cart(dbContext);

            Order order = new Order() { id = 1, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" };
            OrderController controller = new OrderController(new OrderRepository(dbContext, cart), new UserRepository(dbContext), cart);

            //Act

            ViewResult result = (ViewResult) await controller.Checkout(order);

            //Assert

            Assert.That(result.ViewData.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("Your shopping cart must contain items"));

        }
    }
}
