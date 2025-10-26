using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.BuildingBlocks.Domain;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public string CreatedBy { get; protected set; } = "system";
    public string? UpdatedBy { get; protected set; }
    public byte[] RowVersion { get; protected set; } = Array.Empty<byte>();

    public Entity() { }
    
    public void UpdateAudit(DateTime? updatedAt = null, string? updatedBy = null)
    {
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        UpdatedBy = updatedBy ?? "system";
    }
}
