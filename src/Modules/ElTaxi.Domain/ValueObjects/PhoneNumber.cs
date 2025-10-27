using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;

namespace ElTaxi.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }

    private PhoneNumber(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException("CountryCode cannot be empty.", nameof(countryCode));

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("PhoneNumber cannot be empty.", nameof(countryCode));

        if (!countryCode.All(char.IsDigit))
            throw new ArgumentException("CountryCode must contain only digits.");
        if (!number.All(char.IsDigit))
            throw new ArgumentException("PhoneNumber must contain only digits.");

        CountryCode = countryCode;
        Number = number;
    }
    public static PhoneNumber Create(string countryCode, string number)
        => new PhoneNumber(countryCode, number);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }

    public override string ToString()
    {
        return $"+({CountryCode}){Number}";
    }
}
