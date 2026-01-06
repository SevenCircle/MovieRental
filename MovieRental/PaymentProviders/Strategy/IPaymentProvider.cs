namespace MovieRental.PaymentProviders.Strategy;

public interface IPaymentProvider
{
    PaymentMethodType Type { get; }

    Task<bool> Pay(double price);
}
