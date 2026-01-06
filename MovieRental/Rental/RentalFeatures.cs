namespace MovieRental.Rental;

public class RentalFeatures : IRentalFeatures
{
    private readonly MovieRentalDbContext _movieRentalDb;
    public RentalFeatures(MovieRentalDbContext movieRentalDb)
    {
        _movieRentalDb = movieRentalDb;
    }

    /// TODO 2: Make this method async
    /// With 'async' this basically makes it able to not block the thread while waiting for the db to respond. And the 'Await' makes it so that the method will wait for the SaveChangesAsync to complete before proceeding.
    public async Task<Rental> Save(Rental rental)
    {
        // Didnt not make this async mainly because of optimization reasons. 
        // As long as the add does not make a query to the db (i.e. to get the value to a collumn based on another table), it is fine to keep it sync.
        _movieRentalDb.Rentals.Add(rental);
        await _movieRentalDb.SaveChangesAsync();
        return rental;
    }

    /// TODO 3: Include related entities in the query
    public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
    {
        return _movieRentalDb.Rentals
            .AsNoTracking() // read-only; don't track for changes
            .Include(r => r.Costumer)
            .Include(r => r.Movie) // Including Movie as well for more complete data
            .Where(r => r.Costumer != null && r.Costumer.CustomerName == customerName);
    }

    public async Task<bool> ProcessPayment(PaymentMethodType paymentType, double value)
    {
        var paymentMethod = PaymentProviderFactory.Create(paymentType);

        if (paymentMethod == null)
        {
            // Log error or throw exception
            return false;
        }

        return await paymentMethod.Pay(value);
    }
}
