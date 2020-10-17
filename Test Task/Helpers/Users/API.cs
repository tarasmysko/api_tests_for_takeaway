using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Test_Task.Common;
using Test_Task.Helpers;
using static Test_Task.Helpers.Users.Models;

namespace Test_Task.Helpers.Users
{
    public class UsersAPI : Api
    {
        public UsersAPI()
            : base("users")
        {
        }

        public Task<TimeMeasurementResponse<List<UsersResponse>>> GetAllUsersCollection()
        {
            var request = CreateGetRequest();
            return ExecuteRequest<List<UsersResponse>>(request);
        }

        public Task<TimeMeasurementResponse<UsersResponse>> GetUserById(int userId)
        {
            var request = CreateGetRequest(userId.ToString());
            return ExecuteRequest<UsersResponse>(request);
        }
    }
}
