using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Domain.Aggregates;

public sealed class Vehicle : Entity, IAggregateRoot
{
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = null!;
    public int Seats { get; set; }
    public string Color { get; set; } = null!;
    public VehicleType Type { get; set; }

    private Vehicle() { }

    private Vehicle(string make, string model, int year, string licensePlate, int seats, string color, VehicleType type)
    {
        Make = make;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
        Seats = seats;
        Color = color;
        Type = type;
    }
    public static Vehicle Create(string make, string model, int year, string licensePlate, int seats, string color, VehicleType type)
    {
        return new Vehicle(make, model, year, licensePlate, seats, color, type);
    }
}
