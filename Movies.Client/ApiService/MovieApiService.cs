using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/movies");

            request.Content = JsonContent.Create(movie);

            return await SendAsync<Movie>(request);
        }

        public async Task DeleteMovie(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/movies/{id}");

            await SendAsync(request);
        }

        public async Task<Movie> GetMovie(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/movies/{id}");

            return await SendAsync<Movie>(request);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/movies");

            return await SendAsync<List<Movie>>(request);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/movies/{movie.Id}");

            request.Content = JsonContent.Create(movie);

            return await SendAsync<Movie>(request);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                       .ConfigureAwait(false);

            if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception($"Request failed: [{request.Method.Method}] {request.RequestUri?.AbsoluteUri}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            var result = response.StatusCode != System.Net.HttpStatusCode.NotFound ?
                    JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) :
                    default(T);

            return result;
        }

        private async Task SendAsync(HttpRequestMessage request)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                       .ConfigureAwait(false);

            if ((int)response.StatusCode >= 500)
            {
                throw new Exception($"Request failed: [{request.Method.Method}] {request.RequestUri?.AbsoluteUri}");
            }
        }
    }
}
