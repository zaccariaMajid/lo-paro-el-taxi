using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.Application.Services;
using ElTaxi.BuildingBlocks.Application;
using ElTaxi.Domain.Enums;
using ElTaxi.Domain.ValueObjects;

namespace ElTaxi.Application.Interfaces;

public interface IRiderProfileService
{

    Task<Result<RiderCreationResponse>> CreateRiderAsync(Guid userId, string name, string surname, string countryCode, string phoneNumber, PaymentTypes preferredPaymentType, CancellationToken ct = default);
}
