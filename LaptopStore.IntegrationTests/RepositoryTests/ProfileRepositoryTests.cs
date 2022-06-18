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
    public class ProfileRepositoryTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task Create_ShouldAddProfileToDataBase()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user = new User() { id = 1, email = "Test@mail.com", loginName = "Test", password = "Test123", role = Data.Enum.Role.User };
            Profile profile = new Profile() { user = user, userId = 1, age = 20, address = "address", firstName = "test", lastName = "test"};

            // Act

            ProfileRepository repository = new ProfileRepository(dbContext);
            await repository.Create(profile);

            // Assert

            Assert.That(profile, Is.EqualTo(dbContext.Profiles.FirstOrDefault()));
        }

        [Test]
        public async Task GetProfile_ShouldReturnUserNotFound()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            Profile profile1 = new Profile() { user = user1, userId = 1, age = 20, address = "address1", firstName = "test1", lastName = "test1" };
            Profile profile2 = new Profile() { user = user2, userId = 2, age = 22, address = "address2", firstName = "test2", lastName = "test2" };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Profiles.AddAsync(profile1);
            await dbContext.Profiles.AddAsync(profile2);

            await dbContext.SaveChangesAsync();

            // Act

            IBaseResponse<Profile> response = await new ProfileRepository(dbContext).GetProfile("Test3@mail.com");

            // Assert

            Assert.That(response.Description, Is.EqualTo("User not found"));
            Assert.That(response.StatusCode, Is.EqualTo(StatusCode.UserNotFound));
        }

        [Test]
        public async Task GetProfile_ShouldReturnProfile()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            User user1 = new User() { id = 1, email = "Test1@mail.com", loginName = "Test1", password = "Test123", role = Data.Enum.Role.User };
            User user2 = new User() { id = 2, email = "Test2@mail.com", loginName = "Test2", password = "Test123", role = Data.Enum.Role.User };

            Profile profile1 = new Profile() { user = user1, userId = 1, age = 20, address = "address1", firstName = "test1", lastName = "test1" };
            Profile profile2 = new Profile() { user = user2, userId = 2, age = 22, address = "address2", firstName = "test2", lastName = "test2" };

            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Profiles.AddAsync(profile1);
            await dbContext.Profiles.AddAsync(profile2);

            await dbContext.SaveChangesAsync();

            // Act

            IBaseResponse<Profile> response = await new ProfileRepository(dbContext).GetProfile("Test1@mail.com");

            // Assert

            Assert.That(response.Data, Is.EqualTo(profile1));
            Assert.That(response.StatusCode, Is.EqualTo(StatusCode.OK));
        }
        
        [Test]
        public async Task GetAll_ShouldReturnAllProfiles()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Profile> profiles = new()
                {
                    new Profile() { userId = 1, age = 20, address = "address1", firstName = "test1", lastName = "test1" },
                    new Profile() { userId = 2, age = 22, address = "address2", firstName = "test2", lastName = "test2" }
                };

            await dbContext.Profiles.AddRangeAsync(profiles);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<Profile> allProfiles = new ProfileRepository(dbContext).GetAll();

            // Assert

            Assert.That(profiles, Is.EqualTo(allProfiles));
        }

        [Test]
        public async Task Update_ShouldUpdateUser()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();


            Profile profile = new Profile() { userId = 1, age = 20, address = "address1", firstName = "test1", lastName = "test1" };

            string address = "address2";
            string firstName = "test2";
            string lastName = "test2";

            await dbContext.Profiles.AddAsync(profile);

            await dbContext.SaveChangesAsync();

            // Act

            profile.address = address;
            profile.firstName = firstName;
            profile.lastName = lastName;

            Profile updatedProfile = await new ProfileRepository(dbContext).Update(profile);

            // Assert

            Assert.That(address, Is.EqualTo(updatedProfile.address));
            Assert.That(firstName, Is.EqualTo(updatedProfile.firstName));
            Assert.That(lastName, Is.EqualTo(updatedProfile.lastName));
        }

        
        [Test]
        public async Task Delete_ShouldDeleteProfile()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();


            Profile profile = new Profile() { userId = 1, age = 20, address = "address1", firstName = "test1", lastName = "test1" };

            await dbContext.Profiles.AddAsync(profile);

            await dbContext.SaveChangesAsync();

            // Act

            await new ProfileRepository(dbContext).Delete(profile);

            // Assert

            Assert.That(dbContext.Profiles.FirstOrDefault() is null);
        }
    }
}
