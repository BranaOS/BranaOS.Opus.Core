namespace BranaOS.Opus.Core;

public readonly struct Optional<T>
{
    private readonly T? _value;
    public bool IsSet { get; }

    private Optional(T value)
    {
        _value = value;
        IsSet = true;
    }

    public static Optional<T> From(T value) => new(value);
    public static implicit operator Optional<T>(T value) => new(value);

    public bool TryGet(out T value)
    {
        if (IsSet)
        {
            value = _value!;
            return true;
        }
        value = default!;
        return IsSet;
    }

    public void When(Action<T> action)
    {
        if (IsSet) action(_value!);
    }
}

public readonly struct OptionalNullable<T>
{
    private readonly T? _value;
    private bool IsSet { get; }

    private OptionalNullable(T value)
    {
        _value = value;
        IsSet = true;
    }

    public static OptionalNullable<T> From(T value) => new(value);
    public static implicit operator OptionalNullable<T>(T value) => new(value);

    public bool TryGet(out T? value)
    {
        value = IsSet ? _value : default;
        return IsSet;
    }

    public void When(Action<T> action)
    {
        if (IsSet) action(_value!);
    }
}

public static class OptionalExtensions
{
    public static void ApplyTo<T>(this Optional<T> opt, ref T target) where T : notnull
    {
        if (opt.TryGet(out var v)) target = v;
    }

    public static void ApplyTo<T>(this OptionalNullable<T> opt, ref T? target)
    {
        if (opt.TryGet(out var v)) target = v;
    }
}
