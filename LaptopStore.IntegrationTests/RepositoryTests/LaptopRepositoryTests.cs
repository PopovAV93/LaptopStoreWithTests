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
    public class LaptopRepositoryTests
    {

        [Test]
        public async Task Create_ShouldAddLaptopToDataBase()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop = new Laptop() { name = "Test", shortDesc = "Test", longDesc = "Test", price = 2000 };

            // Act

            LaptopRepository repository = new LaptopRepository(dbContext);
            await repository.Create(laptop);

            // Assert

            Assert.That(laptop, Is.EqualTo(dbContext.Laptops.FirstOrDefault()));
        }

        [Test]
        public async Task GetObjectLaptop_ShouldReturnLaptopByLaptopId()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Laptop> laptops = new() {
                new Laptop(),
                new Laptop(),
                new Laptop()
            };


            await dbContext.Laptops.AddRangeAsync(laptops);

            await dbContext.SaveChangesAsync();


            // Act

            Laptop laptop = new LaptopRepository(dbContext).GetObjectLaptop(1);

            // Assert

            Assert.That(laptops.Where(x => x.id == 1).FirstOrDefault(), Is.EqualTo(laptop));
        }

        [Test]
        public async Task GetLaptopsByCategory_ShouldReturnLaptopsByCategory()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Category> categories = new MockLaptopCategories().AllCategories.ToList();

            List<Laptop> laptops = new() {
                new Laptop() { Category = categories[0] },
                new Laptop() { Category = categories[1] },
                new Laptop() { Category = categories[2] }
            };


            await dbContext.Laptops.AddRangeAsync(laptops);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<Laptop> catLaptops = new LaptopRepository(dbContext).GetLaptopsByCategory(categories[0]);

            // Assert

            Assert.That(laptops.Where(x => x.categoryId == categories[0].id), Is.EqualTo(catLaptops));
        }

        [Test]
        public async Task GetFavLaptops_ShouldReturnFavouriteLaptops()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Laptop> laptops = new() {
                new Laptop() { isFavorite = true},
                new Laptop() { isFavorite = false},
                new Laptop() { isFavorite = true} };


            await dbContext.Laptops.AddRangeAsync(laptops);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<Laptop> favLaptops = new LaptopRepository(dbContext).GetFavLaptops();

            // Assert

            Assert.That(laptops.Where(x => x.isFavorite == true), Is.EqualTo(favLaptops));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllLaptops()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Laptop> laptops = new() {
                new Laptop(),
                new Laptop(),
                new Laptop()
            };

            await dbContext.Laptops.AddRangeAsync(laptops);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<Laptop> allLaptops = new LaptopRepository(dbContext).GetAll();

            // Assert

            Assert.That(laptops, Is.EqualTo(allLaptops));
        }

        private AppDBContent GetAppDBContent()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDBContent>().UseInMemoryDatabase("db_test")
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            var context = new AppDBContent(contextOptions);

            return context;
        }
    }
}
