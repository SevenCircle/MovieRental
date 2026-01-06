using MovieRental.Data;

namespace MovieRental.Movie
{
    public class MovieFeatures : IMovieFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        public MovieFeatures(MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }

        public Movie Save(Movie movie)
        {
            _movieRentalDb.Movies.Add(movie);
            _movieRentalDb.SaveChanges();
            return movie;
        }

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        // First one, this method retrieves all movies from the database without any filtering or pagination, which could lead to performance issues if the database contains a large number of movies.
        // Second, it returns a List<Movie>, which may not be the most efficient data structure for all use cases. Depending on how the data will be used, other structures like IEnumerable<Movie> or IQueryable<Movie> might be more appropriate.
        // Third, there is no error handling or logging implemented, which could make it difficult to diagnose issues if the database query fails.
        public List<Movie> GetAll()
        {
            return _movieRentalDb.Movies.ToList();
        }

        // Suggestion to improve the GetAll method by adding pagination support
        // This method allows clients to specify the page number and page size, which helps to limit the amount of data retrieved from the database at once. 
        // And in case the client does not provide these parameters, it defaults to page 1 and a page size of 50.
        public IEnumerable<Movie> GetAll(int pageNumber = 1, int pageSize = 50)
        {
            IEnumerable<Movie> movies = new List<Movie>();

            try
            {
                movies = _movieRentalDb.Movies.Skip(pageSize * (pageNumber - 1)) // Here the pageNumber is subracting 1 to avoid skipping from the 0 position to the pageSize position
                .Take(pageSize);
            }
            catch (Exception ex)
            {
                // Log the exception (in case of debugging is needed or there is a Logging system)
                // For example: _logger.LogError(ex, "An error occurred while retrieving movies with pagination.");
            }

            return movies;
        }
    }
}
