namespace MovieRental.Rental;

public class Rental
{
    [Key]
    public int Id { get; set; }
    public int DaysRented { get; set; }

    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    public Movie.Movie? Movie { get; set; }

    [ForeignKey("Costumer")] //TODO 4: Added ForeignKey attribute by Id and not by name because there can be multiple people with the same name
    public int CostumerId { get; set; }
    public Costumer.Costumer? Costumer { get; set; }

    public PaymentMethodType PaymentMethod { get; set; }

    public double Price { get; set; }

}