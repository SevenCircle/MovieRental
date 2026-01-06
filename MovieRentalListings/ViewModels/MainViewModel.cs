using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MovieRentalListings.Models;

namespace MovieRentalListings.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ListingCustomer> Customers { get; } = new();
    public ObservableCollection<ListingRental> Rentals { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    // Load sample data quickly for UI verification
    public Task LoadSampleDataAsync()
    {
        Customers.Clear();
        Rentals.Clear();

        Customers.Add(new ListingCustomer { Id = 1, Name = "Alice", Email = "alice@example.com" });
        Customers.Add(new ListingCustomer { Id = 2, Name = "Bob", Email = "bob@example.com" });

        Rentals.Add(new ListingRental { Id = 1, CustomerName = "Alice", MovieTitle = "The Matrix", DaysRented = 3, Price = 9.99, PaymentMethodType = "PayPal" });
        Rentals.Add(new ListingRental { Id = 2, CustomerName = "Bob", MovieTitle = "Inception", DaysRented = 1, Price = 3.99, PaymentMethodType = "MBWay" });

        return Task.CompletedTask;
    }

    // Example: fetch lists from a JSON API endpoint (adjust URLs and DTOs to match your API)
    public async Task LoadFromApiAsync(string baseUrl)
    {
        using var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        try
        {
            var custResp = await client.GetStringAsync("/costumers");
            var custs = JsonSerializer.Deserialize<List<ListingCustomer>>(custResp, options);
            if (custs != null)
            {
                Customers.Clear();
                foreach (var c in custs) Customers.Add(c);
            }

            var rentResp = await client.GetStringAsync("/rentals");
            var rents = JsonSerializer.Deserialize<List<ListingRental>>(rentResp, options);
            if (rents != null)
            {
                Rentals.Clear();
                foreach (var r in rents) Rentals.Add(r);
            }
        }
        catch (Exception ex)
        {
            // log or show error — keep simple for now
            Console.WriteLine(ex);
        }
    }
}