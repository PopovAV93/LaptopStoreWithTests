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

namespace LaptopStore.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task Create_ShouldAddUserToDataBase()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user = new User() { email = "Test@mail.com", loginName = "Test", password = "Test123", role = Data.Enum.Role.User };
            
            // Act

            UserRepository repository = new UserRepository(dbContext);
            await repository.Create(user);

            // Assert

            Assert.That(user, Is.EqualTo(dbContext.Users.FirstOrDefault()));
        }

        [Test]
        public async Task UserOrders_ShouldReturnUserOrdersIncludeOrderDetailsAndLaptops()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop1 = new Laptop() { id = 1, name = "Test1", shortDesc = "Test1", longDesc = "Test1", price = 2000 };
            Laptop laptop2 = new Laptop() { id = 2, name = "Test2", shortDesc = "Test2", longDesc = "Test2", price = 2000 };

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            List<Order> orders = new() {
                new Order() { id = 1, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" },
                new Order() { id = 2, userId = 1, address = "test1", firstName = "test1", lastName = "test1", phoneNumber = "123-456-7890" },
                new Order() { id = 3, userId = 2, address = "test2", firstName = "test2", lastName = "test2", phoneNumber = "123-456-7890" }
            };

            List<OrderDetail> orderDetails = new() {
                new OrderDetail() { id = 1, laptopId = 1, laptop = laptop1, orderId = 1, order = orders.FirstOrDefault(x => x.id == 1) },
                new OrderDetail() { id = 2, laptopId = 2, laptop = laptop2, orderId = 1, order = orders.FirstOrDefault(x => x.id == 1) },
                new OrderDetail() { id = 3, laptopId = 1, laptop = laptop1, orderId = 2, order = orders.FirstOrDefault(x => x.id == 2) },
                new OrderDetail() { id = 4, laptopId = 2, laptop = laptop1, orderId = 3, order = orders.FirstOrDefault(x => x.id == 3) }
            };

            await dbContext.Laptops.AddAsync(laptop1);
            await dbContext.Laptops.AddAsync(laptop2);

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Orders.AddRangeAsync(orders);

            await dbContext.OrderDetails.AddRangeAsync(orderDetails);

            await dbContext.SaveChangesAsync();

            // Act

            List<Order> dbOrders = new UserRepository(dbContext).UserOrders("Test1@mail.com").ToList();

            // Assert

            Assert.That(orders.Where(x => x.userId == 1), Is.EqualTo(dbOrders));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<User> users = new()
                {
                    new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User },
                    new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User }
                };

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<User> allUsers = new UserRepository(dbContext).GetAll();

            // Assert

            Assert.That(users, Is.EqualTo(allUsers));
        }

        [Test]
        public async Task Update_ShouldUpdateUser()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();


            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            
            string email = "Test2@mail.com";
            string loginName = "Test2";

            await dbContext.Users.AddAsync(user1);

            await dbContext.SaveChangesAsync();

            // Act

            user1.email = email;
            user1.loginName = loginName;

            User updatedUser = await new UserRepository(dbContext).Update(user1);

            // Assert

            Assert.That(email, Is.EqualTo(updatedUser.email));
            Assert.That(loginName, Is.EqualTo(updatedUser.loginName));
        }

        [Test]
        public async Task Delete_ShouldDeleteUserByUserId()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();


            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };


            await dbContext.Users.AddAsync(user1);

            await dbContext.SaveChangesAsync();

            // Act

            await new UserRepository(dbContext).Delete(user1.id);

            // Assert

            Assert.That(dbContext.Users.FirstOrDefault() is null);
        }

        [Test]
        public async Task Delete_ShouldDeleteUser()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();


            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };


            await dbContext.Users.AddAsync(user1);

            await dbContext.SaveChangesAsync();

            // Act

            await new UserRepository(dbContext).Delete(user1);

            // Assert

            Assert.That(dbContext.Users.FirstOrDefault() is null);
        }
    }
}
