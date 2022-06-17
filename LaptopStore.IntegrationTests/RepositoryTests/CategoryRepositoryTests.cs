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
    public class CategoryRepositoryTests
    {

        [Test]
        public async Task Create_ShouldAddCategoryToDataBase()
        {
            // Arrange

            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Category category = new Category() { categoryName = "Test", desc = "Test" };

            // Act

            CategoryRepository repository = new CategoryRepository(dbContext);
            await repository.Create(category);

            // Assert

            Assert.That(category, Is.EqualTo(dbContext.Categories.FirstOrDefault()));
        }

        [Test]
        public async Task GetCategoryByLaptop_ShouldReturnCategoryByLaptop()
        {
            // Arrange
            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Category> mockCategories = new MockLaptopCategories().AllCategories.ToList();

            Laptop laptop = new Laptop() {
                Category = mockCategories.FirstOrDefault(), 
                categoryId = mockCategories.FirstOrDefault().id, 
                name = "Test" 
            };

            await dbContext.Categories.AddRangeAsync(mockCategories);

            await dbContext.SaveChangesAsync();

            // Act

            Category categorie = new CategoryRepository(dbContext).GetCategoryByLaptop(laptop);

            // Assert

            Assert.That(mockCategories.FirstOrDefault(), Is.EqualTo(categorie));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            AppDBContent dbContext = GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Category> mockCategories = new MockLaptopCategories().AllCategories.ToList();

            await dbContext.Categories.AddRangeAsync(mockCategories);

            await dbContext.SaveChangesAsync();

            // Act

            IQueryable<Category> allCategories = new CategoryRepository(dbContext).GetAll();

            // Assert

            Assert.That(mockCategories, Is.EqualTo(allCategories));
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
