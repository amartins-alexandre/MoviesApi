using MoviesApi.Adapter.DataProvider;
using MoviesApi.Adapter.Model.Setting;
using MoviesApi.Adapter.Repository;
using MoviesApi.Core.DataProvider;
using MoviesApi.Core.Service;
using MoviesApi.Core.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependencie injection to the container.
builder.Services.AddScoped<IAddMovie, AddMovie>();
builder.Services.AddScoped<IUpdateMovie, UpdateMovie>();
builder.Services.AddTransient<IMovieDataProvider, MovieDataProvider>();

// Add MongoDB Settings
builder.Services.Configure<MovieStoreDatabaseSettings>(
    builder.Configuration.GetSection("MovieStoreDatabase")
);
builder.Services.AddSingleton<MovieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
