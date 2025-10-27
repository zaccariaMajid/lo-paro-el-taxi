using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.Aggregates;

public sealed class Ping : Entity, IAggregateRoot
{
    public Guid DriverId { get; set; }
    public string Latitude { get; set; } = null!;
    public string Longitude { get; set; } = null!;
    public decimal Speed { get; set; }
    // Direction in degrees
    public int Heading { get; set; }
    public int Accuracy { get; set; }
    public DateTime SentAt { get; set; }
    private Ping() { }
    private Ping(Guid driverId, string latitude, string longitude, decimal speed, int heading, int accuracy, DateTime sentAt)
    {
        DriverId = driverId;
        Latitude = latitude;
        Longitude = longitude;
        Speed = speed;
        Heading = heading;
        Accuracy = accuracy;
        SentAt = sentAt;
    }

    public Ping Create(Guid DriverId, string latitude, string longitude, decimal speed, int heading, int accuracy, DateTime sentAt)
    {
        return new Ping(DriverId, latitude, longitude, speed, heading, accuracy, sentAt);
    }

    public void UpdateLocation(string latitude, string longitude, decimal speed, int heading, int accuracy, DateTime sentAt)
    {
        Latitude = latitude;
        Longitude = longitude;
        Speed = speed;
        Heading = heading;
        Accuracy = accuracy;
        SentAt = sentAt;
    }
}
