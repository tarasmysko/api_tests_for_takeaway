using FluentAssertions;
using NUnit.Framework;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Test_Task.Helpers.Albums;
using Test_Task.Common;
using System;
using NUnit.Framework.Interfaces;
using System.Linq;
using RazorEngine.Compilation.ImpromptuInterface;

namespace Apis.Albums

{
    [TestFixture, Category("Album Tests")]
    public class AlbumTests
    {
        private readonly AlbumAPI _albumApi;

        protected ReporterHelper extent;

        public AlbumTests()
        {
            _albumApi = new AlbumAPI();
        }

        [OneTimeSetUp]
        public void SetUpReporter()
        {
            extent = new ReporterHelper();
        }

        [SetUp]
        public void StartUpTest()
        {
            extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
                switch (status)
                {
                    case TestStatus.Failed:
                        extent.SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                        Console.WriteLine(TestContext.CurrentContext.Test.FullName);

                        break;

                    case TestStatus.Skipped:
                        extent.SetTestStatusSkipped();
                        break;

                    default:
                        extent.SetTestStatusPass();
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [OneTimeTearDown]
        public void CloseAll()
        {
            try
            {
                extent.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [Test]
        public async Task VerifyAlbumsEndpoint()
        {
            //Arrange
            //Act
            var apiAlbumsResponse = await _albumApi.GetAllAlbumsCollection();

            //Assert
            Assert.Multiple(() =>
            {
                apiAlbumsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                apiAlbumsResponse.Data.Should().NotBeNull();
                apiAlbumsResponse.Data.FirstOrDefault().id.Should().BePositive();
                apiAlbumsResponse.Data.FirstOrDefault().userId.Should().BePositive();
                apiAlbumsResponse.Data.FirstOrDefault().title.Should().NotBeNullOrEmpty();
                Console.WriteLine($"Request execution time {apiAlbumsResponse.ElapsedMiliseconds}");
                apiAlbumsResponse.ContentType.Should().Be("application/json; charset=utf-8");
                apiAlbumsResponse.ContentEncoding.Should().BeNull();
                apiAlbumsResponse.Cookies.Should().NotBeNull();
                apiAlbumsResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                apiAlbumsResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                apiAlbumsResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                apiAlbumsResponse.Headers.Should().NotBeNull();
                //Assert.LessOrEqual(apiAlbumsResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumsResponse.ElapsedMiliseconds}");
            });
        }

        [Test]
        public async Task GetAlbumById()
        {
            //Arrange
            var allAlbums = await _albumApi.GetAllAlbumsCollection();
            var album = allAlbums.Data.FirstOrDefault();
            //Act
            var apiAlbumResponse = await _albumApi.GetAlbumById(album.id);

            //Assert
            Assert.Multiple(() =>
            {
                apiAlbumResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                apiAlbumResponse.Data.Should().NotBeNull();
                apiAlbumResponse.Data.id.Should().BePositive();
                apiAlbumResponse.Data.userId.Should().BePositive();
                apiAlbumResponse.Data.title.Should().NotBeNullOrEmpty();
                apiAlbumResponse.ContentType.Should().Be("application/json; charset=utf-8");
                apiAlbumResponse.ContentEncoding.Should().BeNull();
                apiAlbumResponse.Cookies.Should().NotBeNull();
                apiAlbumResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                apiAlbumResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                apiAlbumResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                apiAlbumResponse.Headers.Should().NotBeNull();
                //Assert.LessOrEqual(apiAlbumResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumResponse.ElapsedMiliseconds}");
                Console.WriteLine($"Request execution time {apiAlbumResponse.ElapsedMiliseconds}");
                apiAlbumResponse.Data.Should().BeEquivalentTo(album);
            });
        }
    }
}
