using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTaxi.BuildingBlocks.Application;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string? Error { get; }
    private Result(bool isSuccess, T value, string? error)
        => (IsSuccess, Value, Error) = (isSuccess, value, error);
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Fail(string error) => new(false, default!, error);
}
