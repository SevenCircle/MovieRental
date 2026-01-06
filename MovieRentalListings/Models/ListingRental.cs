namespace MovieRentalListings.Models;

public class ListingRental
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? MovieTitle { get; set; }
    public int DaysRented { get; set; }
    public double Price { get; set; }
    public string? PaymentMethodType { get; set; }
}