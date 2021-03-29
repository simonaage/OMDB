using Microsoft.AspNetCore.Mvc;
using OMDB_API.Models;
using System.Threading.Tasks;

namespace OMDB_API.Services.Interfaces
{
    public interface IMovieRequest
    {
        Task<ActionResult<Movie>> GetMovieAsync(string apiKey, MovieInput movieInput);
    }
}