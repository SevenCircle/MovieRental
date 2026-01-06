namespace MovieRental.PaymentProviders.Strategy;

public static class PaymentProviderFactory
{
    public static IPaymentProvider Create(PaymentMethodType type) =>
        type switch
        {
            PaymentMethodType.MBWay => new MbWayProvider(),
            PaymentMethodType.PayPal => new PayPalProvider(),
            _ => null
        };
}