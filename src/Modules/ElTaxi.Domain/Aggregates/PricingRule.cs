using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Domain.Aggregates;

public class PricingRule : Entity, IAggregateRoot
{
    public VehicleType VehicleType { get; set; }
    public decimal BaseFareInCents { get; set; }
    public decimal CostPerKmInCents { get; set; }
    public decimal CostPerMinuteInCents { get; set; }
    // Surge multiplier for peak hours or high demand
    public decimal SurgeMultiplier { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime ActiveTo { get; set; }

    private PricingRule() { }

    private PricingRule(VehicleType vehicleType, decimal baseFareInCents, decimal costPerKmInCents, decimal costPerMinuteInCents, decimal surgeMultiplier, DateTime activeFrom, DateTime activeTo)
    {
        VehicleType = vehicleType;
        BaseFareInCents = baseFareInCents;
        CostPerKmInCents = costPerKmInCents;
        CostPerMinuteInCents = costPerMinuteInCents;
        SurgeMultiplier = surgeMultiplier;
        ActiveFrom = activeFrom;
        ActiveTo = activeTo;
    }

    public static PricingRule Create(VehicleType vehicleType, decimal baseFareInCents, decimal costPerKmInCents, decimal costPerMinuteInCents, decimal surgeMultiplier, DateTime activeFrom, DateTime activeTO)
    {
        return new PricingRule(vehicleType, baseFareInCents, costPerKmInCents, costPerMinuteInCents, surgeMultiplier, activeFrom, activeTO);
    }

    public bool IsActive(DateTime dateTime)
    {
        return dateTime >= ActiveFrom && dateTime <= ActiveTo;
    }

    public decimal CalculateFare(decimal distanceInKm, decimal durationInMinutes)
    {
        var fare = BaseFareInCents + (CostPerKmInCents * distanceInKm) + (CostPerMinuteInCents * durationInMinutes);
        fare *= SurgeMultiplier;
        return fare;
    }
}
