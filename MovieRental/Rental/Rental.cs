using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRental.Rental
{
	public class Rental
	{
		[Key]
		public int Id { get; set; }
		public int DaysRented { get; set; }

		[ForeignKey("Movie")]
		public int MovieId { get; set; }
		public Movie.Movie? Movie { get; set; }

		public string PaymentMethod { get; set; }

		[ForeignKey("Costumer")] // Added ForeignKey to link CostumerId to Costumer entity, not the costumer name, mainly because there can be multiple costumers with the same name.
        public int CostumerId{ get; set; }
		public Costumer.Costumer? Costumer{ get; set; }
	}
}
