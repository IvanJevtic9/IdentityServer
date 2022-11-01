using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public interface IMovieApiService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie(int id);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> CreateMovie(Movie movie);
        Task DeleteMovie(int id);
    }
}
