using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.Aggregates;

public class Review : Entity, IAggregateRoot
{
    public Guid RideId { get; set; }
    public Guid ReviewerId { get; set; }
    public Guid RevieweeId { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; } = null!;

    private Review() { }

    private Review(Guid rideId, Guid reviewerId, Guid revieweeId, int rating, string comments)
    {
        RideId = rideId;
        ReviewerId = reviewerId;
        RevieweeId = revieweeId;
        Rating = rating;
        Comments = comments;
        CreatedAt = DateTime.UtcNow;
    }

    public static Review Create(Guid rideId, Guid reviewerId, Guid revieweeId, int rating, string comments)
    {
        return new Review(rideId, reviewerId, revieweeId, rating, comments);
    }
}
