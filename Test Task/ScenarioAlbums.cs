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
    public class AlbumTests
    {
        private readonly AlbumAPI _albumApi;
        private int repetitions;

        public AlbumTests()
        {
            _albumApi = new AlbumAPI();
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            repetitions = ConfigHelper.Repetitions;
        }

        [Test]
        public async Task VerifyBodyForAlbumsEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act

                var apiAlbumsResponse = await _albumApi.GetAllAlbumsCollection();
                var responseBody = apiAlbumsResponse.Data;
                Console.WriteLine($"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");

                //Assert
                Assert.Multiple(() =>
                {
                    apiAlbumsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    apiAlbumsResponse.Data.Should().NotBeNull();
                    responseBody.FirstOrDefault().id.Should().BePositive();
                    responseBody.FirstOrDefault().userId.Should().BePositive();
                    responseBody.FirstOrDefault().title.Should().NotBeNullOrEmpty();
                    //Assert.LessOrEqual(apiAlbumsResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");
                });
            }
        }

        [Test]
        public async Task VerifyCookiesForAlbumsEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act
                var apiAlbumsResponse = await _albumApi.GetAllAlbumsCollection();
                Console.WriteLine($"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");
                //Assert
                Assert.Multiple(() =>
                {
                    apiAlbumsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    apiAlbumsResponse.Cookies.Should().NotBeNull();
                    apiAlbumsResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                    apiAlbumsResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                    apiAlbumsResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                });
            }
        }

        [Test]
        public async Task VerifyHeadersForAlbumsEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act
                var apiAlbumsResponse = await _albumApi.GetAllAlbumsCollection();
                Console.WriteLine($"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");
                //Assert
                Assert.Multiple(() =>
                {
                    apiAlbumsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    apiAlbumsResponse.ContentType.Should().Be("application/json; charset=utf-8");
                    apiAlbumsResponse.ContentEncoding.Should().BeNull();
                    apiAlbumsResponse.Headers.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Set-Cookie").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "CF-RAY").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "cf-request-id").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "NEL").Value.Should().Be("{\"report_to\":\"cf-nel\",\"max_age\":604800}");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Report-To").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "CF-Cache-Status").Value.Should().Be("HIT");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Via").Value.Should().Be("1.1 vegur");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Etag").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiAlbumsResponse, "X-Content-Type-Options").Value.Should().Be("nosniff");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Expires").Value.Should().Be("-1");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Pragma").Value.Should().Be("no-cache");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Cache-Control").Value.Should().Be("max-age=43200");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Access-Control-Allow-Credentials").Value.Should().Be("true");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Vary").Value.Should().Be("Origin, Accept-Encoding");
                    HeadersHelper.HeadersList(apiAlbumsResponse, "Connection").Value.Should().Be("keep-alive");
                });
            }
        }
    }
}
