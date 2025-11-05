using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

using System.Linq;

namespace UnitTests.Pages.Product
{
    public class UpdateTests
    {
        #region TestSetup
        public static UpdateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            TestHelper.MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            TestHelper.MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            TestHelper.MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);
            TestHelper.UrlHelperFactory = new UrlHelperFactory();
            TestHelper.ProductService = new JsonFileProductService(TestHelper.MockWebHostEnvironment.Object);

            pageModel = new UpdateModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext ?? new PageContext(),
                TempData = TestHelper.TempData ?? new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>()),
                Url = TestHelper.UrlHelperFactory?.GetUrlHelper(TestHelper.ActionContext)
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange
            var productId = TestHelper.ProductService.GetAllData().First().Id;

            // Act
            var result = pageModel.OnGet(productId) as PageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(pageModel.Product.Id, Is.EqualTo(productId));
            Assert.That(pageModel.ModelState.IsValid, Is.True);
        }

        [Test]
        public void OnGet_Invalid_Should_Redirect_To_Index()
        {
            // Act
            var result = pageModel.OnGet(null) as RedirectToPageResult;

            // Assert
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }
        #endregion OnGet
        [Test]
        public void OnGet_Unknown_Id_Should_Redirect_To_Index()
        {
            // Act
            var result = pageModel.OnGet("fake123") as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnPost_Null_Product_Should_Not_Throw_And_Redirect_To_Index()
        {
            // Arrange
            pageModel.Product = null;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }
        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Update_And_Redirect()
        {
            // Arrange
            var pageModel = new UpdateModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext,
                ModelState = { }
            };

            pageModel.Product = TestHelper.ProductService.GetAllData().First();
            pageModel.Product.Title = "Updated Title";
            pageModel.ModelState.Clear();

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
            Assert.That(pageModel.ModelState.IsValid, Is.True);
        }

        [Test]
        public void OnPost_Invalid_Model_Should_Return_Page()
        {
            // 
            pageModel.Product = TestHelper.ProductService.GetAllData().First();
            pageModel.ModelState.AddModelError("Title", "Required");

            // Act
            var result = pageModel.OnPost() as PageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }
        #endregion OnPost
    }
}
