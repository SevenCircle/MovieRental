namespace MovieRental.Rental;

public class RentalFeatures : IRentalFeatures
{
    private readonly MovieRentalDbContext _movieRentalDb;
    public RentalFeatures(MovieRentalDbContext movieRentalDb)
    {
        _movieRentalDb = movieRentalDb;
    }

    //With 'async' this basically makes it able to not block the thread while waiting for the db to respond. And the 'Await' makes it so that the method will wait for the SaveChangesAsync to complete before proceeding.
    public async Task<Rental> Save(Rental rental)
    {
        // Didnt not make this async mainly because of optimization reasons. 
        // As long as the add does not make a query to the db (i.e. to get the value to a collumn based on another table), it is fine to keep it sync.
        _movieRentalDb.Rentals.Add(rental);
        await _movieRentalDb.SaveChangesAsync();
        return rental;
    }

    public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
    {
        return _movieRentalDb.Rentals
            .Include(r => r.Costumer)
            .Include(r => r.Movie)
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
