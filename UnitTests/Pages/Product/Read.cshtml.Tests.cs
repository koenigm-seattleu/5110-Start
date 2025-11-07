using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using Bunit;

namespace UnitTests.Pages.Product
{
    [TestFixture]
    public class ReadTests : BunitTestContext
    {
        [Test]
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange
            var webHostEnvironment = TestHelper.MockWebHostEnvironment.Object;
            var productService = new JsonFileProductService(webHostEnvironment);
            var pageModel = new ReadModel(productService);

            // Act
            pageModel.OnGet("jenlooper-light");

            // Assert
            Assert.That(pageModel.Product, Is.Not.Null);
            Assert.That(pageModel.Product.Id, Is.EqualTo("jenlooper-light"));
        }
    }
}