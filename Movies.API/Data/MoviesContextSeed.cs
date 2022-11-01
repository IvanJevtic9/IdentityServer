using Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.API.Data
{
    public class MoviesContextSeed
    {
        public async static void SeedAsync(MoviesAPIContext context)
        {
            if (!context.Movie.Any())
            {
                var movies = new List<Movie>
                {
                    new ()
                    {
                        Id = 1,
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    },
                    new()
                    {
                        Id = 2,
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    },
                    new()
                    {
                        Id = 3,
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    },
                    new()
                    {
                        Id = 4,
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        Owner = "alice"
                    }
                };

                await context.Movie.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }
        }
    }
}
