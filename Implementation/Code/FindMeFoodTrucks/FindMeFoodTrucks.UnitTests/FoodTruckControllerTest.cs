using FindMeFoodTrucks.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
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
