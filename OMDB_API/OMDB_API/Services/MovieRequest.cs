using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OMDB_API.Models;
using OMDB_API.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMDB_API.Services
{
    public class MovieRequest : IMovieRequest
    {
        private readonly IHttpClientFactory _clientFactory;

        public MovieRequest(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ActionResult<Movie>> GetMovieAsync(string apiKey, MovieInput movieInput)
        {
            var client = _clientFactory.CreateClient();
            string url = UriCreator.GetOmdbUri(movieInput, apiKey);

            var response = await client.GetAsync(url);
            var responseData = response.Content.ReadAsStringAsync().Result;
            Movie movie = JsonConvert.DeserializeObject<Movie>(responseData);

            return new OkObjectResult(movie);
        }
    }
}
