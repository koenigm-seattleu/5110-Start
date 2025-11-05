using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Bunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.Product
{
    [TestFixture]
    public class ReadTests
    {
        private ReadModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadModel(TestHelper.ProductService);
        }

        [Test]
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange
            var productId = TestHelper.ProductService.GetAllData().First().Id;

            // Act
            var result = pageModel.OnGet(productId) as PageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(pageModel.Product, Is.Not.Null);
            Assert.That(pageModel.Product.Id, Is.EqualTo(productId));
        }

        [Test]
        public void OnGet_Null_Id_Should_Redirect_To_Index()
        {
            // Act
            var result = pageModel.OnGet(null) as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Index()
        {
            // Act
            var result = pageModel.OnGet("invalid-id") as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }
    }
}