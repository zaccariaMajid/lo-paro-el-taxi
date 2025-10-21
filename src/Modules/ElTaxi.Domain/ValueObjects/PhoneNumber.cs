using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PhoneNumber cannot be empty.", nameof(value));
        Value = value.Trim().ToLowerInvariant();
    }
    public static PhoneNumber Create(string value) => new PhoneNumber(value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
