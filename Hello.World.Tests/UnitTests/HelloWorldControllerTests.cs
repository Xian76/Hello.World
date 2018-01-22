using Hello.World.Api.Controllers;
using Hello.World.Api.Interfaces;
using Hello.World.Api.Tests.UnitTests.Shared;
using Moq;
using NUnit.Framework;

namespace Hello.World.Api.Tests.UnitTests
{
    /// <summary>
    /// Unit Tests for the HelloWorldController
    /// </summary>
    [TestFixture]
    public class HelloWorldControllerTests : CommonTestMethods
    {
        /// <summary>
        /// The mocked Text File service
        /// </summary>
        private Mock<IDataService> dataServiceMock;
        /// <summary>
        /// The implementation to test
        /// </summary>
        private HelloWorldController helloWorldController;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [OneTimeSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            dataServiceMock = new Mock<IDataService>();

            // Create object to test
            helloWorldController = new HelloWorldController(dataServiceMock.Object);
        }

        #region HelloWorldController GetData Test
        /// <summary>
        ///     Tests the controller's get method for success
        /// </summary>
        [Test]
        public void UnitTestTodaysDataControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetSampleHelloWorldData();

            // Set up dependencies
            dataServiceMock.Setup(m => m.GetData()).Returns(expectedResult);

            // Call the method to test
            var result = helloWorldController.GetData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result, expectedResult.Message);
        }

        #endregion
    }
}