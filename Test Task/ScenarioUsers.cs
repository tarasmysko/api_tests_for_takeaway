using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test_Task.Common;
using Test_Task.Helpers.Users;

namespace Apis.Users

{
    [TestFixture, Category("Users"), Parallelizable(ParallelScope.All)]
    public class UserTests
    {
        private readonly UsersAPI _usersApi;
        private int repetitions;

        public UserTests()
        {
            _usersApi = new UsersAPI();
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            repetitions = ConfigHelper.Repetitions;
        }

        [Test]
        public async Task VerifyBodyForAllUsersEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act
                var apiUsersResponse = await _usersApi.GetAllUsersCollection();
                var responseBody = apiUsersResponse.Data;
                var userObject = responseBody.FirstOrDefault();

                //Assert
                Assert.Multiple(() =>
                {
                    apiUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    responseBody.Should().NotBeNull();
                    userObject.id.Should().BePositive();
                    userObject.name.Should().NotBeNullOrEmpty();
                    userObject.username.Should().NotBeNullOrEmpty();
                    userObject.email.Should().NotBeNullOrEmpty();
                    userObject.phone.Should().NotBeNullOrEmpty();
                    userObject.website.Should().NotBeNullOrEmpty();
                    userObject.address.street.Should().NotBeNullOrEmpty();
                    userObject.address.suite.Should().NotBeNullOrEmpty();
                    userObject.address.city.Should().NotBeNullOrEmpty();
                    userObject.address.zipcode.Should().NotBeNullOrEmpty();
                    userObject.address.geo.lat.Should().NotBeNullOrEmpty();
                    userObject.address.geo.lng.Should().NotBeNullOrEmpty();
                    userObject.company.name.Should().NotBeNullOrEmpty();
                    userObject.company.catchPhrase.Should().NotBeNullOrEmpty();
                    userObject.company.bs.Should().NotBeNullOrEmpty();
                    Console.WriteLine($"Request execution time {apiUsersResponse.ElapsedMiliseconds} ms");
                });
            }
        }

        [Test]
        public async Task VerifyHeadersForAllUsersEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act
                var apiUsersResponse = await _usersApi.GetAllUsersCollection();

                //Assert
                Assert.Multiple(() =>
                {
                    apiUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    apiUsersResponse.ContentType.Should().Be("application/json; charset=utf-8");
                    apiUsersResponse.ContentEncoding.Should().BeNull();
                    apiUsersResponse.Headers.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "Set-Cookie").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "CF-RAY").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "cf-request-id").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "NEL").Value.Should().Be("{\"report_to\":\"cf-nel\",\"max_age\":604800}");
                    HeadersHelper.HeadersList(apiUsersResponse, "Report-To").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "CF-Cache-Status").Value.Should().Be("HIT");
                    HeadersHelper.HeadersList(apiUsersResponse, "Via").Value.Should().Be("1.1 vegur");
                    HeadersHelper.HeadersList(apiUsersResponse, "Etag").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(apiUsersResponse, "X-Content-Type-Options").Value.Should().Be("nosniff");
                    HeadersHelper.HeadersList(apiUsersResponse, "Expires").Value.Should().Be("-1");
                    HeadersHelper.HeadersList(apiUsersResponse, "Pragma").Value.Should().Be("no-cache");
                    HeadersHelper.HeadersList(apiUsersResponse, "Cache-Control").Value.Should().Be("max-age=43200");
                    HeadersHelper.HeadersList(apiUsersResponse, "Access-Control-Allow-Credentials").Value.Should().Be("true");
                    HeadersHelper.HeadersList(apiUsersResponse, "Vary").Value.Should().Be("Origin, Accept-Encoding");
                    HeadersHelper.HeadersList(apiUsersResponse, "Connection").Value.Should().Be("keep-alive");
                    Console.WriteLine($"Request execution time {apiUsersResponse.ElapsedMiliseconds} ms");
                    //Assert.LessOrEqual(apiUsersResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiUsersResponse.ElapsedMiliseconds} ms");
                });
            }
        }

        [Test]
        public async Task VerifyCookiesForAllUsersEndpoint()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange
                //Act
                var apiUsersResponse = await _usersApi.GetAllUsersCollection();

                //Assert
                Assert.Multiple(() =>
                {
                    apiUsersResponse.Cookies.Should().NotBeNull();
                    apiUsersResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                    apiUsersResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                    apiUsersResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                    Console.WriteLine($"Request execution time {apiUsersResponse.ElapsedMiliseconds} ms");
                });
            }
        }
    }
}
