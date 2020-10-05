using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
    /// <summary>
    /// Query test
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CosmosDALTest
    {

        [Fact]
        public void QueryData_ValidResults_True()
        {
            ///Arrange
            const string QUERY = "Test Query";
            var mockCon = new Mock<Container>();
            var myItems = new List<FoodFacilityResponse>();

            var item = new FoodFacilityResponse();
            myItems.Add(item);

            var feedResponseMock = new Mock<FeedResponse<FoodFacilityResponse>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(myItems.GetEnumerator());

            var feedIteratorMock = new Mock<FeedIterator<FoodFacilityResponse>>();
            feedIteratorMock.Setup(f => f.HasMoreResults).Returns(true);
            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));
            var qd = new QueryDefinition(QUERY);
            mockCon.Setup(c => c.GetItemQueryIterator<FoodFacilityResponse>(It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>())).Returns(feedIteratorMock.Object);

            ///Act 
            CosmosDAL cDAL = new CosmosDAL(null, null, mockCon.Object, null);
            var result = cDAL.QueryData(qd).Result;

            ///Assert
            Assert.Equal(myItems.Count, result.Count);
        }

        /// <summary>
        /// Update record test
        /// </summary>
        [Fact]
        public void WriteData_Update_True()
        {
            ///Arrange
            var mockCon = new Mock<Container>();
            var myItems = new List<FoodFacility>();
            var mockLogger = new Mock<ILogger>();

            var item = new FoodFacility();
            item.objectid = "0";
            item.latitude = "0";
            item.longitude = "0";

            myItems.Add(item);

            var responseMock = new Mock<ItemResponse<FoodFacility>>();

            mockCon.Setup(c => c.ReadItemAsync<FoodFacility>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(responseMock.Object));

            try
            {
                ///Act 
                CosmosDAL cDAL = new CosmosDAL(null, null, mockCon.Object, mockLogger.Object);
                cDAL.WriteData(myItems).Wait();
            }
            catch
            {
                ///Assert
                Assert.True(false);
            }
        }

        /// <summary>
        /// Insert record test
        /// </summary>
        [Fact]
        public void WriteData_Insert_True()
        {
            ///Arrange
            var mockCon = new Mock<Container>();
            var myItems = new List<FoodFacility>();
            var mockLogger = new Mock<ILogger>();

            var item = new FoodFacility();
            item.objectid = "0";
            item.latitude = "0";
            item.longitude = "0";

            myItems.Add(item);

            var responseMock = new Mock<ItemResponse<FoodFacility>>();

            mockCon.Setup(c => c.ReadItemAsync<FoodFacility>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            try
            {
                ///Act 
                CosmosDAL cDAL = new CosmosDAL(null, null, mockCon.Object, mockLogger.Object);
                cDAL.WriteData(myItems).Wait();
            }
            catch
            {
                ///Assert
                Assert.True(false);
            }
        }
    }
}
