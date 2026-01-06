namespace MovieRental.Costumer;

public interface ICostumerFeatures
{
	Task<Costumer> Save(Costumer costumer);
    IEnumerable<Costumer> GetAll(int pageNumber, int pageSize);
}