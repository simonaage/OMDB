using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OMDB_API.Attributes;
using OMDB_API.Models;
using OMDB_API.Services;
using OMDB_API.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OMDBController : ControllerBase
    {
        private readonly IMovieRequest _movieRequest;

        public OMDBController(IMovieRequest movieRequest)
        {
            _movieRequest = movieRequest;
        }
        [HttpGet]
        [ApiKeyAuth]
        [Produces("application/json", "application/xml")]
        [Route("movie")]
        public async Task<ActionResult<Movie>> GetAsync([FromHeader(Name = "apikey")][Required] string apiKey, [FromQuery(Name = "title")][Required] string title, [FromQuery(Name = "year")] int year, [FromQuery(Name = "plotsize")] PlotSize plotSize)
        {
            MovieInput movieInput = new MovieInput(title, year, plotSize);

            try
            {
                var movieRequest = await _movieRequest.GetMovieAsync(apiKey, movieInput);
                return movieRequest;
            }
            catch (HttpRequestException)
            {
                return new ContentResult
                {
                    Content = "Unable to connect to OMDB",
                    ContentType = "text/plain",
                    StatusCode = 400
                };
            }
            catch (Exception e)
            {
                return new ContentResult
                {
                    Content = e.Message,
                    ContentType = "text/plain",
                    StatusCode = 500
                };
            }
        }
    }
}
