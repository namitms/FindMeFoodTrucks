using FindMeFoodTrucks.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
    /// <summary>
    /// FoodTruckController test class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FoodTruckControllerTest
    {
        [Fact]
        public void Get_GetTasks_NotNull()
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object);

            ///Act 
            var result = ftController.Get();

            ///Assert
            Assert.NotNull(result);
        }

    }
}
