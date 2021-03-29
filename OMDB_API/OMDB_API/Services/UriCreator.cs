using OMDB_API.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace OMDB_API.Services
{
    public static class UriCreator
    {
        public static string GetOmdbUri(MovieInput movieInput, string apiKey)
        {
            string url = "https://www.omdbapi.com";
            UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Port = -1; //to delete port number
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["t"] = movieInput.Title;
            query["y"] = movieInput.Year.ToString();
            query["plot"] = movieInput.PlotSize.ToString();
            query["apikey"] = apiKey; //fix this
            uriBuilder.Query = query.ToString();
            url = uriBuilder.ToString();
            return url;
        }
    }
}
