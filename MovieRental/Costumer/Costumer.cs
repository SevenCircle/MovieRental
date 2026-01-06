using System.ComponentModel.DataAnnotations;

namespace MovieRental.Costumer
{
	public class Costumer
	{
		[Key]
		public int Id { get; set; }
		public string CustomerName { get; set; } = string.Empty;
	}
}
