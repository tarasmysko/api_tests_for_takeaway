using System.Threading.Tasks;
using Test_Task.Common;
using static Test_Task.Helpers.Posts.Models;

namespace Test_Task.Helpers.Posts
{
    public class PostsAPI : Api
    {
        public PostsAPI()
            : base("posts")
        {
        }

        public Task<TimeMeasurementResponse<PostResponse>> GetPostById(int postId)
        {
            var request = CreateGetRequest(postId.ToString());
            return ExecuteRequest<PostResponse>(request);
        }

        public Task<TimeMeasurementResponse<PostResponse>> CreatePost(PostResponse post)
        {
            var request = CreatePostRequest(post);
            return ExecuteRequest<PostResponse>(request);
        }
    }
}
