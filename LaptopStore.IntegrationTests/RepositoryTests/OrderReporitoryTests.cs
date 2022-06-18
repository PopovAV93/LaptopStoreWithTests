using LaptopStore.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Net;
using System.Net.Http;
using LaptopStore.Data.Models;
using Microsoft.Extensions.Configuration;
using LaptopStore.Data.Repository;
using LaptopStore.Data.Mocks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using LaptopStore.Data.Response;
using LaptopStore.Data.Enum;

namespace LaptopStore.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task Create_ShouldAddOrderAndOrderDetailsToDataBase()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop1 = new Laptop() { id = 1, name = "Test1", shortDesc = "Test1", longDesc = "Test1", price = 2000 };
            Laptop laptop2 = new Laptop() { id = 2, name = "Test2", shortDesc = "Test2", longDesc = "Test2", price = 2000 };

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            Order order = new Order() { id = 1, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" };

            List<OrderDetail> orderDetails = new() {
                new OrderDetail() { id = 1, laptopId = 1, laptop = laptop1, orderId = 1, order = order, price = 2000 },
                new OrderDetail() { id = 2, laptopId = 2, laptop = laptop2, orderId = 1, order = order, price = 2000 }
            };

            await dbContext.Laptops.AddAsync(laptop1);
            await dbContext.Laptops.AddAsync(laptop2);

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);
                        
            await dbContext.SaveChangesAsync();

            Cart cart = new Cart(dbContext);
            cart.AddToCart(laptop1);
            cart.AddToCart(laptop2);
            cart.ListCartItems = cart.GetCartItems();

            // Act

            OrderRepository repository = new OrderRepository(dbContext, cart);
            await repository.Create(order);

            // Assert

            Assert.That(order, Is.EqualTo(dbContext.Orders.FirstOrDefault()));
            Assert.That(orderDetails.FirstOrDefault(x => x.id == 1).laptopId, Is.EqualTo(dbContext.OrderDetails.FirstOrDefault(x => x.id == 1).laptopId));
            Assert.That(orderDetails.FirstOrDefault(x => x.id == 2).laptopId, Is.EqualTo(dbContext.OrderDetails.FirstOrDefault(x => x.id == 2).laptopId));
        }

        
        [Test]
        public async Task GetAll_ShouldReturnAllOrders()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            List<Order> orders = new() {
                new Order() { id = 1, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" },
                new Order() { id = 2, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" },
                new Order() { id = 3, userId = 2, address = "test2", firstName = "test2", lastName = "test2", phoneNumber = "123-456-7890" }
            };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Orders.AddRangeAsync(orders);

            await dbContext.SaveChangesAsync();
            Cart cart = new Cart(dbContext);

            // Act

            IQueryable<Order> allOrders = new OrderRepository(dbContext, cart).GetAll();

            // Assert

            Assert.That(orders, Is.EqualTo(allOrders));
        }

        [Test]
        public async Task Delete_ShouldDeleteOrder()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            
            Order order = new Order() { id = 1, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" };

            await dbContext.Users.AddAsync(user1);

            await dbContext.Orders.AddAsync(order);

            await dbContext.SaveChangesAsync();

            Cart cart = new Cart(dbContext);

            // Act

            await new OrderRepository(dbContext, cart).Delete(order);

            // Assert

            Assert.That(dbContext.Orders.FirstOrDefault() is null);
        }
    }
}