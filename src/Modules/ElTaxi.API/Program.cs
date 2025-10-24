using System.Security.AccessControl;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Infrastructure;
using ElTaxi.Infrastructure.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connString = builder.Configuration.GetConnectionString("Postgres")
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:Postgres");
builder.Services.AddSingleton(new NpgsqlConnection(connString));

// Register every reopository that impelements IBaseRepository without extra implemented methods
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Reguster repositories with extra methods
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();