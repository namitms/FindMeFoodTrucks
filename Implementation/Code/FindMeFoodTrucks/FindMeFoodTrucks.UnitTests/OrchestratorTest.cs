using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Ingestor.Helpers;
using FindMeFoodTrucks.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
    /// <summary>
    /// WebHelperTest test class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OrchestratorTest
    {
        /// <summary>
        /// Test ingest orchestration
        /// </summary>
        [Fact]
        public void SynchronizeData_Valid_True()
        {
            ///Arrange

            var mockConfig = new Mock<IConfiguration>();
            var mockCDAL = new Mock<CosmosDAL>(new object[] { null, null, null });
            var myItems = new List<FoodFacility>();
            var mockLogger = new Mock<ILogger>();
            var mockWebHelper = new Mock<WebRequestHelper>(new object[] { mockLogger.Object, null });

            var item = new FoodFacility();
            item.objectid = "0";
            item.latitude = "0";
            item.longitude = "0";

            myItems.Add(item);

            mockWebHelper.Setup(m => m.GetResponse(It.IsAny<string>())).Returns(Task.FromResult(JsonConvert.SerializeObject(myItems)));
            mockCDAL.Setup(m => m.WriteData(It.IsAny<List<FoodFacility>>()));

            ///Act 
            Orchestrator orc = new Orchestrator();
            var res = orc.SynchronizeData(mockWebHelper.Object, mockCDAL.Object, mockConfig.Object, mockLogger.Object);

            ///Assert
            Assert.NotNull(res);

        }
    }
}
