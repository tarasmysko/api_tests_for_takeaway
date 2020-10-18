using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test_Task.Common;
using Test_Task.Helpers.Posts;
using static Test_Task.Helpers.Posts.Models;

namespace Apis.Posts

{
    [TestFixture, Category("Posts"), Parallelizable(ParallelScope.All)]
    public class PostTests
    {
        private readonly PostsAPI _postsApi;

        private PostResponse post;
        private int repetitions;

        public PostTests()
        {
            _postsApi = new PostsAPI();
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            repetitions = ConfigHelper.Repetitions;
        }

        [SetUp]
        public void StartUpTest()
        {
            post = new PostResponse
            {
                title = "takeaway",
                body = "time to order food",
                userId = 12345
            };
            //"title": "takeaway",
            //"body": "time to order food",
            //"userId": 12345,
        }

        [Test]
        public async Task VerifyCreatePostResponseBody()
        {
            for (int i = 0; i < repetitions; i++)
            {
                //Arrange

                //Act
                var createPostResponse = await _postsApi.CreatePost(post);
                var responseBody = createPostResponse.Data;
                var headers = createPostResponse.Headers;
                var search = "CF-RAY";
                var result = headers.FirstOrDefault(s => s.Name == search);
                var b = HeadersHelper.HeadersList(createPostResponse, "Etag");
                //Assert
                Assert.Multiple(() =>
                {
                    createPostResponse.StatusCode.Should().Be(HttpStatusCode.Created);
                    responseBody.id.Should().BePositive();
                    responseBody.title.Should().Be(post.title);
                    responseBody.body.Should().Be(post.body);
                    responseBody.userId.Should().Be(post.userId);
                    Console.WriteLine($"Request execution time {createPostResponse.ElapsedMiliseconds} ms");
                });
            }
        }

        [Test]
        public async Task VerifyCreatePostResponseHeaders()
        {
            for (int i = 0; i < repetitions; i++)
            {
                //Arrange

                //Act
                var createPostResponse = await _postsApi.CreatePost(post);
                var responseBody = createPostResponse.Data;

                //Assert
                Assert.Multiple(() =>
                {
                    createPostResponse.ContentType.Should().Be("application/json; charset=utf-8");
                    createPostResponse.ContentEncoding.Should().BeNull();
                    createPostResponse.Headers.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "CF-RAY").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "Set-Cookie").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "cf-request-id").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "NEL").Value.Should().Be("{\"report_to\":\"cf-nel\",\"max_age\":604800}");
                    HeadersHelper.HeadersList(createPostResponse, "Report-To").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "CF-Cache-Status").Value.Should().Be("DYNAMIC");
                    HeadersHelper.HeadersList(createPostResponse, "Via").Value.Should().Be("1.1 vegur");
                    HeadersHelper.HeadersList(createPostResponse, "Etag").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "X-Content-Type-Options").Value.Should().Be("nosniff");
                    HeadersHelper.HeadersList(createPostResponse, "Location").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "Access-Control-Expose-Headers").Value.Should().NotBeNull();
                    HeadersHelper.HeadersList(createPostResponse, "Expires").Value.Should().Be("-1");
                    HeadersHelper.HeadersList(createPostResponse, "Pragma").Value.Should().Be("no-cache");
                    HeadersHelper.HeadersList(createPostResponse, "Cache-Control").Value.Should().Be("no-cache");
                    HeadersHelper.HeadersList(createPostResponse, "Access-Control-Allow-Credentials").Value.Should().Be("true");
                    HeadersHelper.HeadersList(createPostResponse, "Vary").Value.Should().Be("Origin, X-HTTP-Method-Override, Accept-Encoding");
                    HeadersHelper.HeadersList(createPostResponse, "Connection").Value.Should().Be("keep-alive");
                    Console.WriteLine($"Request execution time {createPostResponse.ElapsedMiliseconds} ms");
                    //Assert.LessOrEqual(apiAlbumsResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");
                });
            }
        }

        [Test]
        public async Task VerifyCreatePostResponseCookies()
        {
            for (int i = 0; i < repetitions; i++)
            {
                Console.WriteLine($"Test will run {repetitions} time(s)");
                //Arrange

                //Act
                var createPostResponse = await _postsApi.CreatePost(post);
                var responseBody = createPostResponse.Data;

                //Assert

                Assert.Multiple(() =>
                {
                    createPostResponse.Cookies.Should().NotBeNull();
                    createPostResponse.Cookies.FirstOrDefault().Expired.Should().BeFalse();
                    createPostResponse.Cookies.FirstOrDefault().Expires.Should().NotBeSameDateAs(DateTime.Today);
                    createPostResponse.Cookies.FirstOrDefault().Name.Should().Be("__cfduid");
                    Console.WriteLine($"Request execution time {createPostResponse.ElapsedMiliseconds} ms ms");
                    //Assert.LessOrEqual(apiAlbumsResponse.ElapsedMiliseconds, 200.00, $"Request execution time {apiAlbumsResponse.ElapsedMiliseconds} ms");
                });
            }
        }
    }
}
