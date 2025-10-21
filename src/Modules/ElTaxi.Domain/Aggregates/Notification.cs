using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Domain.Aggregates;

public class Notification : Entity, IAggregateRoot
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime SentAt { get; set; }

    private Notification() { }

    private Notification(Guid userId, NotificationType type, string title, string message)
    {
        UserId = userId;
        Type = type;
        Title = title;
        Message = message;
        IsRead = false;
        SentAt = DateTime.UtcNow;
    }

    public static Notification Create(Guid userId, NotificationType type, string title, string message)
    {
        return new Notification(userId, type, title, message);
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
