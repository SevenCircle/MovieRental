namespace MovieRental.PaymentProviders;

public class MbWayProvider : IPaymentProvider
{
    public PaymentMethodType Type => PaymentMethodType.MBWay;

    public Task<bool> Pay(double price)
    {
        //ignore this implementation
        return Task.FromResult<bool>(true);
    }
}
