using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UnitTests.Pages.Product
{
    public class DeleteTests
    {
        private DeleteModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
                Url = TestHelper.UrlHelperFactory.GetUrlHelper(TestHelper.ActionContext),
            };
        }

        [Test]
        public void OnGet_Valid_Id_Should_Return_Product()
        {
            var product = TestHelper.ProductService.GetAllData().First();
            var result = pageModel.OnGet(product.Id);
            Assert.That(pageModel.Product, Is.Not.Null);
            Assert.That(pageModel.Product.Id, Is.EqualTo(product.Id));
        }

        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Index()
        {
            var result = pageModel.OnGet("invalid") as RedirectToPageResult;
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnPost_Valid_Should_Delete_And_Redirect()
        {
            var product = TestHelper.ProductService.GetAllData().First();
            pageModel.Product = product;
            var result = pageModel.OnPost() as RedirectToPageResult;
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }
        
        [Test]
        public void OnGet_Null_Id_Should_Redirect_To_Index()
        {
            // Arrange + Act
            var result = pageModel.OnGet(null) as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnPost_Null_Product_Id_Should_Redirect_To_Index()
        {
            // Arrange
            pageModel.Product = new ContosoCrafts.WebSite.Models.ProductModel { Id = null };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnPost_Null_Product_Should_Redirect_To_Index()
        {
            // Arrange
            pageModel.Product = null;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }
    }
}