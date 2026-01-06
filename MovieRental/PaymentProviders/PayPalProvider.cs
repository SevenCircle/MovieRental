namespace MovieRental.PaymentProviders;

public class PayPalProvider : IPaymentProvider
{
    public PaymentMethodType Type => PaymentMethodType.PayPal;

    public Task<bool> Pay(double price)
    {
        //ignore this implementation
        return Task.FromResult<bool>(true);
    }
}
