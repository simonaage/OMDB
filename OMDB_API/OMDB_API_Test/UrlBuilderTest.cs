using OMDB_API.Models;
using OMDB_API.Services;
using Xunit;

namespace OMDB_API_Test
{
    public class UrlBuilderTest
    {
        private readonly MovieInput _movieInput;

        public UrlBuilderTest()
        {
            _movieInput = new MovieInput("Joker", 2000, PlotSize.Short);
        }
        [Fact]
        public void GetUrlStringOutputTest()
        {
            //arrange
            string expected = "https://www.omdbapi.com/?t=Joker&y=2000&plot=Short&apikey=testkey";
            //act
            string actual = UriCreator.GetOmdbUri(_movieInput, "testkey");
            //assert
            Assert.Equal(expected, actual);
        }
    }
}
