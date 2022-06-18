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
using Microsoft.Extensions.Logging;
using LaptopStore.Data.Response;
using System.Security.Claims;
using LaptopStore.ViewModels;
using LaptopStore.Data.Enum;
using LaptopStore.Data.Helpers;

namespace LaptopStore.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task Register_ShouldReturnUserIsAlreadyExists()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.SaveChangesAsync();

            RegisterViewModel model = new() { email = "Test1@mail.com", loginName = "Test1", password = "123456", passwordConfirm = "123456" };

            // Act
            AccountRepository repository = new AccountRepository(new UserRepository(dbContext), new ProfileRepository(dbContext), new Logger<AccountRepository>(new LoggerFactory()));
            IBaseResponse<ClaimsIdentity> response = await repository.Register(model);

            // Assert

            Assert.That(response.Description, Is.EqualTo("There is already a user with this email"));
        }

        [Test]
        public async Task Register_ShouldCreateUserAndProfile()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            RegisterViewModel model = new() { email = "Test1@mail.com", loginName = "Test1", password = "123456", passwordConfirm = "123456" };

            // Act
            AccountRepository repository = new AccountRepository(new UserRepository(dbContext), new ProfileRepository(dbContext), new Logger<AccountRepository>(new LoggerFactory()));
            IBaseResponse<ClaimsIdentity> response = await repository.Register(model);

            // Assert

            Assert.That(response.Description, Is.EqualTo("Object added"));
            Assert.That(response.StatusCode, Is.EqualTo(StatusCode.OK));
            Assert.That(dbContext.Users.FirstOrDefault() is not null && dbContext.Profiles.FirstOrDefault() is not null);
        }

        [Test]
        public async Task Login_ShouldReturnUserIsNotFound()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.SaveChangesAsync();

            LoginViewModel model = new() { email = "Test3@mail.com", password = "123456"};

            // Act
            AccountRepository repository = new AccountRepository(new UserRepository(dbContext), new ProfileRepository(dbContext), new Logger<AccountRepository>(new LoggerFactory()));
            IBaseResponse<ClaimsIdentity> response = await repository.Login(model);

            // Assert

            Assert.That(response.Description, Is.EqualTo("User is not found"));
        }

        [Test]
        public async Task Login_ShouldReturnWrongPassword()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.SaveChangesAsync();

            LoginViewModel model = new() { email = "Test1@mail.com", password = "123456789" };

            // Act
            AccountRepository repository = new AccountRepository(new UserRepository(dbContext), new ProfileRepository(dbContext), new Logger<AccountRepository>(new LoggerFactory()));
            IBaseResponse<ClaimsIdentity> response = await repository.Login(model);

            // Assert

            Assert.That(response.Description, Is.EqualTo("Wrong Password"));
        }

        [Test]
        public async Task Login_ShouldReturnOK()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = HashPasswordHelper.HashPassword("Test123"), role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = HashPasswordHelper.HashPassword("Test123"), role = Data.Enum.Role.User };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.SaveChangesAsync();

            LoginViewModel model = new() { email = "Test1@mail.com", password = "Test123" };

            // Act
            AccountRepository repository = new AccountRepository(new UserRepository(dbContext), new ProfileRepository(dbContext), new Logger<AccountRepository>(new LoggerFactory()));
            IBaseResponse<ClaimsIdentity> response = await repository.Login(model);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(StatusCode.OK));
        }
    }
}
