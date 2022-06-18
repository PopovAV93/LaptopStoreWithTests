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

namespace LaptopStore.IntegrationTests
{
    [TestFixture]
    public class CartTests
    {
        private readonly TestHelper testHelper = new();

        [Test]
        public async Task AddToCart_ShouldAddCartItemsToDataBase()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop = new Laptop() { name = "Test", shortDesc = "Test", longDesc = "Test", price = 2000 };
            
            // Act

            Cart cart = new Cart(dbContext);
            cart.AddToCart(laptop);

            // Assert

            Assert.That(dbContext.CartItems.FirstOrDefault() is not null);
        }

        [Test]
        public async Task DeleteFromCart_ShouldDeleteCartItem()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop = new Laptop() { name = "Test", shortDesc = "Test", longDesc = "Test", price = 2000 };
            CartItem cartItem = new CartItem { laptop = laptop, price = laptop.price };
            
            await dbContext.CartItems.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            // Act

            Cart cart = new Cart(dbContext);
            cart.DeleteFromCart(cartItem);

            // Assert

            Assert.That(dbContext.CartItems.FirstOrDefault() is null);
        }

        [Test]
        public async Task GetCartItems_ShouldReturnCartItems()
        {
            // Arrange

            AppDBContent dbContext = testHelper.GetAppDBContent();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            Laptop laptop = new Laptop() { name = "Test", shortDesc = "Test", longDesc = "Test", price = 2000 };
            CartItem cartItem1 = new CartItem { laptop = laptop, price = laptop.price };
            
            await dbContext.CartItems.AddAsync(cartItem1);
            await dbContext.SaveChangesAsync();

            // Act

            Cart cart = new Cart(dbContext);
            CartItem cartItem2 = cart.GetCartItems().FirstOrDefault();

            // Assert

            Assert.That(cartItem2, Is.EqualTo(cartItem1));
        }
    }
}
