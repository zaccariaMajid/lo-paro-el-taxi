using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.BuildingBlocks.Domain;

public sealed record Result<T>(bool IsSuccess, T Value, string? Error);
public static class Result
{
    public static Result<T> Success<T>(T value) => new Result<T>(true, value, null);
    public static Result<T> Fail<T>(string error) => new Result<T>(false, default!, error);
}
