using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.AddRating
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        //[Test]
        //public void AddRating_InValid_....()
        //{
        //    // Arrange

        //    // Act
        //    //var result = TestHelper.ProductService.AddRating(null, 1);

        //    // Assert
        //    //Assert.AreEqual(false, result);
        //}

        // ....

        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddRating_Valid_Product_Rating_5_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().First();

            // Assert
            Assert.That(result, Is.True);
            Assert.That(dataNewList.Ratings.Length, Is.EqualTo(countOriginal + 1));
            Assert.That(dataNewList.Ratings.Last(), Is.EqualTo(5));
        }
        #endregion AddRating
        
        #region GetAllData
        [Test]
        public void GetAllData_Should_Return_Products()
        {
            // Act
            var result = TestHelper.ProductService.GetAllData();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
        #endregion GetAllData
        
        #region CreateData
        [Test]
        public void CreateData_Valid_Should_Create_New_Product()
        {
            // Arrange
            var oldCount = TestHelper.ProductService.GetAllData().Count();

            // Act
            var result = TestHelper.ProductService.CreateData();
            var newCount = TestHelper.ProductService.GetAllData().Count();

            // Assert
            Assert.That(result, Is.Not.Null, "Created product should not be null");
            Assert.That(result.Id, Is.Not.Empty, "Created product should have an ID");
            Assert.That(newCount, Is.EqualTo(oldCount + 1), "Product count should increase by 1");
        }
        #endregion CreateData
        
    }
}