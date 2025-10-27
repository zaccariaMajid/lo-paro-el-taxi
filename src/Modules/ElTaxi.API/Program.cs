using System.Net.NetworkInformation;
using System.Security.AccessControl;
using ElTaxi.Application.Interfaces;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Interfaces;
using ElTaxi.Infrastructure;
using ElTaxi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connString = builder.Configuration.GetConnectionString("Postgres")
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:Postgres");

builder.Services.AddDbContext<ElTaxiDbContext>(options =>
    options.UseNpgsql(connString));

//Register generic repostiory
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Register repositories
builder.Services.AddScoped(typeof(IDriverProfileRepository), typeof(DriverProfileRepository));
builder.Services.AddScoped(typeof(INotificationRepository), typeof(INotificationRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
builder.Services.AddScoped(typeof(IPingRepository), typeof(PingRepository));
builder.Services.AddScoped(typeof(IPricingRuleRepository), typeof(PricingRuleRepository));
builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));
builder.Services.AddScoped(typeof(IRiderProfileRepository), typeof(RiderProfileRepository));
builder.Services.AddScoped(typeof(IRideRepository), typeof(RideRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IVehicleRepository), typeof(VehicleRepository));

// Add services
builder.Services.AddSingleton<IPasswordService, PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();