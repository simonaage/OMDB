using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using OMDB_API.Controllers;
using OMDB_API.Models;
using OMDB_API.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OMDB_API_Test
{
    public class MovieRequestTest
    {
        private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly MovieRequest _movieRequest;
        private readonly Mock<HttpMessageHandler> _mockHandler;
        private readonly Movie _movie;
        private readonly MovieInput _movieInput;
        private readonly string json;

        public MovieRequestTest()
        {
            _mockClientFactory = new Mock<IHttpClientFactory>(); //create mocks
            _movieRequest = new MovieRequest(_mockClientFactory.Object);
            _mockHandler = new Mock<HttpMessageHandler>();
            _movie = new Movie()
            {
                Title = "Joker",
                Year = "2000",
                Plot = "Royal Circus, owned by Govindan, is on rocks. With the help of his manager Khader, Govindan just manages to run the company, though not in a satisfactory manner. All the members of the ..."
            };
            json = JsonConvert.SerializeObject(_movie);
            _movieInput = new MovieInput("Joker", 2000, PlotSize.Short);
        }
        [Fact]
        public async Task GetMovieFromOmdbAsyncTest()
        {
            //arrange
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json)
                }); //mock of statuscode and content

            var client = new HttpClient(_mockHandler.Object);
            _mockClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client); //mock to create httpclient request
            OkObjectResult expectedResult = new OkObjectResult(_movie);

            //act
            var controller = new MovieRequest(_mockClientFactory.Object);
            var result = await controller.GetMovieAsync("testkey", _movieInput);
            _mockClientFactory.Verify(f => f.CreateClient(It.IsAny<String>()), Times.Once); //checking that client was called once

            //assert
            Assert.NotNull(result); //Check that object isn't null
            Assert.IsAssignableFrom<ActionResult<Movie>>(result); //Check that object is ActionResult<Movie>
            Assert.Equal(expectedResult.ToString(), result.Result.ToString()); //Check that return is OkObjectResult
        }
    }
}
