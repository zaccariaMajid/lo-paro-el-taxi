namespace ElTaxi.Domain.ValueObjects;

public enum RideStatus
{
    Requested = 1,
    Accepted,
    Arriving,
    InProgress,
    Completed,
    Canceled
}
