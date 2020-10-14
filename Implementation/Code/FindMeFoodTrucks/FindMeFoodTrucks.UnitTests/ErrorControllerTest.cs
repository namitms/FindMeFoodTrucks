using FindMeFoodTrucks.WebAPI.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace FindMeFoodTrucks.UnitTests
{
    /// <summary>
    /// ErrorController test class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ErrorControllerTest
    {
        /// <summary>
        /// Null Exception test
        /// </summary>
        [Fact]
        public void ErrorLocalDevelopment_ThorwNullReferenceException()
        {
            ///Arrange
            var mockHostEnvironment = new Mock<IWebHostEnvironment>();
            mockHostEnvironment.Setup(c => c.EnvironmentName).Returns("Development");
            ErrorController erController = new ErrorController();

            ///Act and Assert
            Assert.Throws<NullReferenceException>(() => erController.ErrorLocalDevelopment(mockHostEnvironment.Object));
        }

        /// <summary>
        /// InvalidOperationException test
        /// </summary>
        [Fact]
        public void ErrorLocalDevelopment_ThorwInvalidOperationException()
        {
            ///Arrange
            var mockHostEnvironment = new Mock<IWebHostEnvironment>();
            ErrorController erController = new ErrorController();

            ///Act and Assert
            Assert.Throws<InvalidOperationException>(() => erController.ErrorLocalDevelopment(mockHostEnvironment.Object));
        }


        /// <summary>
        /// InvalidOperationException test
        /// </summary>
        [Fact]
        public void Error_ThorwNullReferenceException()
        {
            ///Arrange
            var mockHostEnvironment = new Mock<IWebHostEnvironment>();
            ErrorController erController = new ErrorController();

            ///Act and Assert
            Assert.Throws<NullReferenceException>(() => erController.Error());
        }
    }
}
