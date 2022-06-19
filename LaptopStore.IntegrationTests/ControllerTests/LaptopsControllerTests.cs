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
using LaptopStore.ViewModels;
using Microsoft.Extensions.Configuration;
using LaptopStore.Data.Repository;
using LaptopStore.Data.Mocks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using LaptopStore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class LaptopsControllerTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task List_ShouldGetLaptopListByCategory()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Category> mockCategories = new MockLaptopCategories().AllCategories.ToList();
            List<Laptop> mockLaptops = new MockLaptops().getLaptops.ToList();

            
            await dbContext.Categories.AddRangeAsync(mockCategories);
            await dbContext.Laptops.AddRangeAsync(mockLaptops);
            await dbContext.SaveChangesAsync();

            // Act
            LaptopRepository laptopRepository = new LaptopRepository(dbContext);
            CategoryRepository categorieRepository = new CategoryRepository(dbContext);
            LaptopsController controller = new LaptopsController(laptopRepository, categorieRepository);

            ViewResult result = controller.List(mockCategories[0].categoryName);

            LaptopListViewModel resultModel = (LaptopListViewModel) result.Model;

            // Assert

            Assert.That(resultModel.allLaptops, Is.EqualTo(dbContext.Laptops.Where(x => x.categoryId == mockCategories[0].id)));
        }

        [Test]
        public async Task LaptopDetails_ShouldGetLaptopDetailsByLaptopId()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            List<Laptop> mockLaptops = new MockLaptops().getLaptops.ToList();


            await dbContext.Laptops.AddRangeAsync(mockLaptops);
            await dbContext.SaveChangesAsync();

            // Act
            LaptopRepository laptopRepository = new LaptopRepository(dbContext);
            CategoryRepository categorieRepository = new CategoryRepository(dbContext);
            LaptopsController controller = new LaptopsController(laptopRepository, categorieRepository);

            ViewResult result = controller.LaptopDetails(mockLaptops[0].id);

            // Assert

            Assert.That(mockLaptops[0], Is.EqualTo(result.Model));
        }
    }
}
