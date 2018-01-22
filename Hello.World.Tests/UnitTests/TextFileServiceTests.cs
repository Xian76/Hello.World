using System;
using System.Configuration;
using Hello.World.Api.Interfaces;
using Hello.World.Api.Services;
using Hello.World.Api.Tests.UnitTests.Shared;
using Moq;
using NUnit.Framework;

namespace Hello.World.Api.Tests.UnitTests
{
    /// <summary>
    /// Unit Tests for the TextFileService
    /// </summary>
    [TestFixture]
    public class TextFileServiceTests : CommonTestMethods
    {
        /// <summary>
        /// The mocked Web API Settings
        /// </summary>
        private Mock<IWebApiSettings> webApiSettingMock;
        /// <summary>
        /// The mocked File Reader service
        /// </summary>
        private Mock<IFileReader> fileReaderServiceMock;
        /// <summary>
        /// The implementation to test
        /// </summary>
        private IDataService textFileService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [OneTimeSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            webApiSettingMock = new Mock<IWebApiSettings>();
            fileReaderServiceMock = new Mock<IFileReader>();

            // Create object to test
            textFileService = new TextFileService(webApiSettingMock.Object, fileReaderServiceMock.Object);
        }

        #region TextFileService Tests
        /// <summary>
        ///     Tests the class's GetData method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSuccess()
        {
            const string dataFileLocation = "\\data\\HelloWorldData.txt";
            const string fileContents = "Hello World";

            // Create the expected result
            var expectedResult = GetSampleHelloWorldData();

            // Set up dependencies
            webApiSettingMock.Setup(m => m.DataFileLocation).Returns(dataFileLocation);
            fileReaderServiceMock.Setup(m => m.ReadAllText(dataFileLocation)).Returns(fileContents);

            // Call the method to test
            var result = textFileService.GetData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Message, expectedResult.Message);
        }

        /// <summary>
        /// Tests for a null DataFileLocation in the web config
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceDataFileLocationNull()
        {
            const string dataFileLocation = null;
            const string fileContents = "Hello World";

            // Set up dependencies
            webApiSettingMock.Setup(m => m.DataFileLocation).Returns(dataFileLocation);
            fileReaderServiceMock.Setup(m => m.ReadAllText(dataFileLocation)).Returns(fileContents);

            // Call the method to test
            Assert.Throws<SettingsPropertyNotFoundException>(() => textFileService.GetData());
        }

        /// <summary>
        /// Tests for an empty DataFileLocation in the web config
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceDataFileLocationEmpty()
        {
            string dataFileLocation = string.Empty;
            const string fileContents = "Hello World";

            // Set up dependencies
            webApiSettingMock.Setup(m => m.DataFileLocation).Returns(dataFileLocation);
            fileReaderServiceMock.Setup(m => m.ReadAllText(dataFileLocation)).Returns(fileContents);

            // Call the method to test
            Assert.Throws<SettingsPropertyNotFoundException>(() => textFileService.GetData());
        }

        /// <summary>
        /// Tests for a null file returned by the file service
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceFileContentsNull()
        {
            const string dataFileLocation = "\\data\\HelloWorldData.txt";
            const string fileContents = null;

            // Set up dependencies
            webApiSettingMock.Setup(m => m.DataFileLocation).Returns(dataFileLocation);
            fileReaderServiceMock.Setup(m => m.ReadAllText(dataFileLocation)).Returns(fileContents);

            // Call the method to test
            Assert.Throws<ArgumentNullException>(() => textFileService.GetData());
        }

        /// <summary>
        /// Tests for an empty file returned by the file service
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceFileContentsEmpty()
        {
            // Create return models for dependencies
            const string dataFileLocation = "\\data\\HelloWorldData.txt";
            string fileContents = string.Empty;

            // Set up dependencies
            webApiSettingMock.Setup(m => m.DataFileLocation).Returns(dataFileLocation);
            fileReaderServiceMock.Setup(m => m.ReadAllText(dataFileLocation)).Returns(fileContents);

            // Call the method to test
            Assert.Throws<ArgumentNullException>(() => textFileService.GetData());
        }
        #endregion
    }
}