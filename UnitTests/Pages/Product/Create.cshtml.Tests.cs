using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Pages.Product
{
    public class CreateTests
    {
        #region TestSetup
        public static CreateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Arrange
            pageModel = new CreateModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
                Url = new Microsoft.AspNetCore.Mvc.Routing.UrlHelper(TestHelper.ActionContext)
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Create_New_Product_And_Redirect()
        {
            // Act
            var result = pageModel.OnGet() as RedirectToPageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PageName, Is.EqualTo("/Product/Update"));
            Assert.That(result.RouteValues.ContainsKey("id"), Is.True);
        }
        #endregion OnGet
    }
}