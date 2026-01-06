namespace MovieRental.Costumer;

public class CostumerFeatures : ICostumerFeatures
{
    private readonly MovieRentalDbContext _movieRentalDb;
    public CostumerFeatures(MovieRentalDbContext movieRentalDb)
    {
        _movieRentalDb = movieRentalDb;
    }

    public async Task<Costumer> Save(Costumer costumer)
    {
        _movieRentalDb.Costumers.Add(costumer);
        await _movieRentalDb.SaveChangesAsync();
        return costumer;
    }

    public IEnumerable<Costumer> GetAll(int pageNumber = 1, int pageSize = 50)
    {
        IEnumerable<Costumer> costumers = new List<Costumer>();

        try
        {
            costumers = _movieRentalDb.Costumers.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);
        }
        catch (Exception ex)
        {
            // Log the exception (in case of debugging is needed or there is a Logging system)
            // For example: _logger.LogError(ex, "An error occurred while retrieving movies with pagination.");
        }

        return costumers;
    }
}