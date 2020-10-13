using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using FindMeFoodTrucks.WebAPI.Controllers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
    /// <summary>
    /// FoodTruckController test class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FoodTruckControllerTest
    {
        /// <summary>
        /// Null Exception test
        /// </summary>
        [Fact]
        public void Get_FoodTrucks_ThorwNullReferenceException()
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            string search = string.Empty;
            long rad = 0;
            double lat = 0;
            double lon = 0;
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object, null);

            ///Act and Assert
            Assert.ThrowsAsync<NullReferenceException>(() => ftController.Get(rad, lon, lat, search));
        }

        /// <summary>
        /// Test heartbeat endpoint
        /// </summary>
        [Fact]
        public void Get_FoodTrucks_Heartbeat_Alive()
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            var mockCDAL = new Mock<CosmosDAL>(new object[] { null, null, null });
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object, mockCDAL.Object);

            ///Act
            var result =  ftController.Get();

            ///Assert
            Assert.Equal(true,result);
        }


        /// <summary>
        /// Out of range Exception test
        /// </summary>
        [Theory]
        [InlineData(0, 181, 0)]
        [InlineData(0, -181, 0)]
        [InlineData(91, 0, 0)]
        [InlineData(-91, 0, 0)]
        [InlineData(91, 0, -1)]
        public void Get_FoodTrucks_ThorwArgumentOutOfRangeException(double lat, double  lon, long rad)
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            var mockCDAL = new Mock<CosmosDAL>(new object[] { null, null, null });
            var response = new List<FoodFacilityResponse>();
            mockCDAL.Setup(c => c.QueryData(It.IsAny<QueryDefinition>())).Returns(Task.FromResult(response));
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object, mockCDAL.Object);
            string search = string.Empty;


            ///Act and Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => ftController.Get(rad, lon, lat, search));
        }

        /// <summary>
        /// Empty search string test
        /// </summary>
        [Fact]
        public void Get_FoodTrucks_ValidResults_WithoutSearchstring()
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            var mockCDAL = new Mock<CosmosDAL>(new object[] { null, null, null });
            var response = new List<FoodFacilityResponse>();
            mockCDAL.Setup(c => c.QueryData(It.IsAny<QueryDefinition>())).Returns(Task.FromResult(response));
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object, mockCDAL.Object);
            long rad = 0;
            double lat = 0;
            double lon = 0;
            string search = string.Empty;

            ///Act 
            var result = ftController.Get(rad, lon, lat, search);

            ///Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// With search string test
        /// </summary>
        [Fact]
        public void Get_FoodTrucks_ValidResults_WithSearchString()
        {
            ///Arrange
            var mockLogger = new Mock<ILogger<FoodTruckController>>();
            var mockConfig = new Mock<IConfiguration>();
            var mockCDAL = new Mock<CosmosDAL>(new object[] { null, null, null });
            var response = new List<FoodFacilityResponse>();
            mockCDAL.Setup(c => c.QueryData(It.IsAny<QueryDefinition>())).Returns(Task.FromResult(response));
            FoodTruckController ftController = new FoodTruckController(mockLogger.Object, mockConfig.Object, mockCDAL.Object);
            long rad = 0;
            double lat = 0;
            double lon = 0;
            string search = "Test String";

            ///Act 
            var result = ftController.Get(rad, lon, lat, search);

            ///Assert
            Assert.NotNull(result);
        }
    }
}
