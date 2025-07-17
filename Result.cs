using System;

namespace BranaOS.Opus.Core;

public abstract record Error(string Message, string Key)
{
  public string Message { get; init; } = Message;
  public string Key { get; init; } = Key;
}

public class Result<T>
{
  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;
  public T Value { get; }
  public IEnumerable<Error> Errors { get; }
  internal Result(T value)
  {
    IsSuccess = true;
    Value = value;
    Errors = [];
  }

  internal Result(IEnumerable<Error> errors)
  {
    IsSuccess = false;
    Errors = errors.OfType<Error>();
    Value = default!;
  }
}

public static class Result
{
  public static Result<T> Ok<T>(T value) => new(value);
  public static Result<T> Fail<T>(Error error) => new([error]);
  public static Result<T> Fail<T>(IEnumerable<Error> errors) => new(errors);
}
