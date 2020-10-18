using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test_Task.Helpers.Users;

namespace Apis.Users
{
    [TestFixture, Category("Users"), Parallelizable(ParallelScope.All)]
    public class UserByIdTests
    {
        private readonly UsersAPI _usersApi;

        public UserByIdTests()
        {
            _usersApi = new UsersAPI();
        }

        [Test]
        public async Task VerifyBodyForGetUserByIdEndpoint()
        {
            //Arrange
            var allUsers = await _usersApi.GetAllUsersCollection();
            var user = allUsers.Data.FirstOrDefault();
            //Act
            var userResponse = await _usersApi.GetUserById(user.id);
            var userResponseBody = userResponse.Data;

            //Assert
            Assert.Multiple(() =>
            {
                userResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                userResponseBody.Should().NotBeNull();
                userResponseBody.id.Should().BePositive();
                userResponseBody.name.Should().NotBeNullOrEmpty();
                userResponseBody.username.Should().NotBeNullOrEmpty();
                userResponseBody.email.Should().NotBeNullOrEmpty();
                userResponseBody.phone.Should().NotBeNullOrEmpty();
                userResponseBody.website.Should().NotBeNullOrEmpty();
                userResponseBody.address.street.Should().NotBeNullOrEmpty();
                userResponseBody.address.suite.Should().NotBeNullOrEmpty();
                userResponseBody.address.city.Should().NotBeNullOrEmpty();
                userResponseBody.address.zipcode.Should().NotBeNullOrEmpty();
                userResponseBody.address.geo.lat.Should().NotBeNullOrEmpty();
                userResponseBody.address.geo.lng.Should().NotBeNullOrEmpty();
                userResponseBody.company.name.Should().NotBeNullOrEmpty();
                userResponseBody.company.catchPhrase.Should().NotBeNullOrEmpty();
                userResponseBody.company.bs.Should().NotBeNullOrEmpty();
                Console.WriteLine($"Request execution time {userResponse.ElapsedMiliseconds} ms");
            });
        }

        [Test]
        public async Task VerifyHeadersForGetUserByIdEndpoint()
        {
            //Arrange
            var allUsers = await _usersApi.GetAllUsersCollection();
            var user = allUsers.Data.FirstOrDefault();
            //Act
            var userResponse = await _usersApi.GetUserById(user.id);
            //Assert
            Assert.Multiple(() =>
            {
                userResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                userResponse.ContentType.Should().Be("application/json; charset=utf-8");
                userResponse.ContentEncoding.Should().BeNull();
                userResponse.Headers.Should().NotBeNull();
                Console.WriteLine($"Request execution time {userResponse.ElapsedMiliseconds} ms");
                //Assert.LessOrEqual(apiUsersResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiUsersResponse.ElapsedMiliseconds} ms");
            });
        }

        [Test]
        public async Task VerifyCookiesForGetUserByIdEndpoint()
        {
            //Arrange
            var allUsers = await _usersApi.GetAllUsersCollection();
            var user = allUsers.Data.FirstOrDefault();
            //Act
            var userResponse = await _usersApi.GetUserById(user.id);

            //Assert
            Assert.Multiple(() =>
            {
                userResponse.Cookies.Should().NotBeNull();
                userResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                userResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                userResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                Console.WriteLine($"Request execution time {userResponse.ElapsedMiliseconds} ms");
            });
        }
    }
}
