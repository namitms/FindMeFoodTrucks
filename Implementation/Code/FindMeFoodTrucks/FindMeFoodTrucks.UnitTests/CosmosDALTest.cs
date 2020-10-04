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
    [ExcludeFromCodeCoverage]
    public class CosmosDALTest
    {

        [Fact]
        public void Get_QueryData_ValidResults()
        {
            ///Arrange
            const string QUERY = "Test Query";
            var mockCon = new Mock<Container>();
            var myItems = new List<FoodFacility>();

            var item = new FoodFacility();
            myItems.Add(item);

            var feedResponseMock = new Mock<FeedResponse<FoodFacility>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(myItems.GetEnumerator());

            var feedIteratorMock = new Mock<FeedIterator<FoodFacility>>();
            feedIteratorMock.Setup(f => f.HasMoreResults).Returns(true);
            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            mockCon.Setup(c => c.GetItemQueryIterator<FoodFacility>(It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>())).Returns(feedIteratorMock.Object);

            ///Act 
            CosmosDAL cDAL = new CosmosDAL(null, null, mockCon.Object, null);
            var result = cDAL.QueryData(QUERY).Result;

            ///Assert
            Assert.Equal(myItems.Count, result.Count);
        }

        [Fact]
        public void Get_WriteData_Update()
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
                cDAL.WriteData(myItems);
            }
            catch
            {
                ///Assert
                Assert.True(false);
            }
        }

        [Fact]
        public void Get_WriteData_Insert()
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
                cDAL.WriteData(myItems);
            }
            catch
            {
                ///Assert
                Assert.True(false);
            }
        }
    }
}
