using MovieRental.Costumer;
using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

/// The DbContext is registered with a scoped lifetime by default when using AddDbContext<TContext>() while the RentalFeature was as Singleton so it was a mismatch in terms of life time.
/// Changing the RentalFeature to Scoped resolves this issue, and using Scoped is appropriate here since it aligns with the DbContext's lifetime and there is no need for it to be Singleton of that same class and this way there is one instance per request.
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

// Adding Movie and Costumer Features, the movie feature was missing in the original code and the costumer feature was created by me.
builder.Services.AddScoped<IMovieFeatures, MovieFeatures>();
builder.Services.AddScoped<ICostumerFeatures, CostumerFeatures>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
