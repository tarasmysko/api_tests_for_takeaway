using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Test_Task.Common;
using Test_Task.Helpers;
using static Test_Task.Helpers.Albums.Models;

namespace Test_Task.Helpers.Albums
{
    public class AlbumAPI : Api
    {
        public AlbumAPI()
            : base("albums")
        {
        }

        public Task<TimeMeasurementResponse<List<AlbumResponse>>> GetAllAlbumsCollection()
        {
            var request = CreateGetRequest();
            return ExecuteRequest<List<AlbumResponse>>(request);
        }

        public Task<TimeMeasurementResponse<AlbumResponse>> GetAlbumById(int albumId)
        {
            var request = CreateGetRequest(albumId.ToString());
            return ExecuteRequest<AlbumResponse>(request);
        }
    }
}
