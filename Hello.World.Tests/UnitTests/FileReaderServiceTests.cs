using Hello.World.Api.Interfaces;
using Hello.World.Api.Services;
using Hello.World.Api.Tests.UnitTests.Shared;
using NUnit.Framework;

namespace Hello.World.Api.Tests.UnitTests
{
    /// <summary>
    /// Unit Tests for the FileReaderService
    /// </summary>
    [TestFixture()]
    public class FileReaderServiceTests : CommonTestMethods
    {
        /// <summary>
        /// The implementation to test
        /// </summary>
        private IFileReader fileReaderService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [OneTimeSetUp]
        public void InitTestSuite()
        {
            // Create object to test
            fileReaderService = new FileReaderService();
        }

        #region TextFileService Tests
        /// <summary>
        ///     Tests the class's ReadAllText method for success
        /// </summary>
        [Test]
        public void UnitTestFileReaderServiceReadAllTextSuccess()
        {
            // Create return models for dependencies
            const string dataFileLocation = "\\data\\HelloWorldData.txt";

            // Create the expected result
            var expectedResult = GetSampleHelloWorldDataAsString();

            // Call the method to test
            var result = fileReaderService.ReadAllText(dataFileLocation);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result, expectedResult);
        }
        #endregion
    }
}