using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        Value = value.Trim().ToLowerInvariant();
    }
    public static Email Create(string value) => new Email(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
