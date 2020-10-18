using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test_Task.Common;
using Test_Task.Helpers.Albums;

namespace Apis.Albums

{
    [TestFixture, Category("Albums"), Parallelizable(ParallelScope.All)]
    public class AlbumByIdTests
    {
        private readonly AlbumAPI _albumApi;
        private int repetitions;

        public AlbumByIdTests()
        {
            _albumApi = new AlbumAPI();
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            repetitions = ConfigHelper.Repetitions;
        }

        [Test]
        public async Task GetAlbumById()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");

                //Arrange
                var allAlbums = await _albumApi.GetAllAlbumsCollection();
                var album = allAlbums.Data.FirstOrDefault();

                //Act
                var apiAlbumResponse = await _albumApi.GetAlbumById(album.id);
                Console.WriteLine($"Request execution time {apiAlbumResponse.ElapsedMiliseconds} ms");

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
                    HeadersHelper.HeadersList(apiAlbumResponse, "Set-Cookie").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumResponse, "CF-RAY").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumResponse, "cf-request-id").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumResponse, "NEL").Value.Should().Be("{\"report_to\":\"cf-nel\",\"max_age\":604800}");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Report-To").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumResponse, "CF-Cache-Status").Value.Should().Be("HIT");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Via").Value.Should().Be("1.1 vegur");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Etag").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumResponse, "X-Content-Type-Options").Value.Should().Be("nosniff");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Expires").Value.Should().Be("-1");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Pragma").Value.Should().Be("no-cache");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Cache-Control").Value.Should().Be("max-age=43200");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Access-Control-Allow-Credentials").Value.Should().Be("true");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Vary").Value.Should().Be("Origin, Accept-Encoding");
                    HeadersHelper.HeadersList(apiAlbumResponse, "Connection").Value.Should().Be("keep-alive");
                    apiAlbumResponse.Cookies.Should().NotBeNull();
                    apiAlbumResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                    apiAlbumResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                    apiAlbumResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                    //Assert.LessOrEqual(apiAlbumResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumResponse.ElapsedMiliseconds} ms");
                });
            }
        }
    }
}
