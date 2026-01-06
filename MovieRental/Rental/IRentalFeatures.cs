namespace MovieRental.Rental;

public interface IRentalFeatures
{
	Task<Rental> Save(Rental rental);
	IEnumerable<Rental> GetRentalsByCustomerName(string customerName);
	Task<bool> ProcessPayment(PaymentMethodType rental, double value);
}