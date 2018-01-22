using System.Configuration;
using Hello.World.Api.Tests.UnitTests.Shared;
using Hello.World.ConsoleApp.Interfaces;
using Hello.World.ConsoleApp.Services;
using Moq;
using NUnit.Framework;

namespace Hello.World.Api.Tests.UnitTests
{
    /// <summary>
    /// Unit Tests for the GetWebApiDataService
    /// The Web Api must be running for these tests to pass!
    /// </summary>
    [TestFixture]
    public class GetWebApiDataServiceTests : CommonTestMethods
    {
        /// <summary>
        /// The mocked Console Application settings
        /// </summary>
        private Mock<IConsoleAppSettings> consoleAppSettingsMock;
        /// <summary>
        /// The implementation to test
        /// </summary>
        private IGetDataService getWebApiDataService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [OneTimeSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            consoleAppSettingsMock = new Mock<IConsoleAppSettings>();

            // Create object to test
            getWebApiDataService = new GetWebApiDataService(consoleAppSettingsMock.Object);
        }

        #region GetWebApiDataService Tests
        /// <summary>
        ///     Tests the class's GetData method for success
        /// </summary>
        [Test]
        public void UnitTestGetWebApiDataServiceGetDataSuccess()
        {
            // Create return models for dependencies
            const string webApiLocation = "http://localhost:19730/api/";

            // Create the expected result
            var expectedResult = GetSampleHelloWorldDataAsString();

            // Set up dependencies
            consoleAppSettingsMock.Setup(m => m.WebApiLocation).Returns(webApiLocation);

            // Call the method to test
            var result = getWebApiDataService.GetData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result, expectedResult);
        }

        /// <summary>
        /// Tests for a null WebApiLocation in the app config
        /// </summary>
        [Test]
        public void UnitTestGetWebApiDataServiceGetDataWebApiLocationNull()
        {
            // Create return models for dependencies
            const string webApiLocation = null;

            // Set up dependencies
            consoleAppSettingsMock.Setup(m => m.WebApiLocation).Returns(webApiLocation);

            // Call the method to test
            Assert.Throws<SettingsPropertyNotFoundException>(() => getWebApiDataService.GetData());
        }

        /// <summary>
        /// Tests for an empty WebApiLocation in the app config
        /// </summary>
        [Test]
        public void UnitTestGetWebApiDataServiceGetDataWebApiLocationEmpty()
        {
            // Create return models for dependencies
            string webApiLocation = string.Empty;

            // Set up dependencies
            consoleAppSettingsMock.Setup(m => m.WebApiLocation).Returns(webApiLocation);

            // Call the method to test
            Assert.Throws<SettingsPropertyNotFoundException>(() => getWebApiDataService.GetData());
        }
        #endregion
    }
}