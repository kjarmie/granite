namespace ErrorHandling;

public record Result
{
}

/// <summary>
/// Represents the outcome of an operation, either containing a successful value or an error.
/// </summary>
/// <typeparam name="T">The type of the value if successful.</typeparam>
public record Result<T> : Result
{
    /// <summary>
    /// The successful value, if present.
    /// </summary>
    private readonly T _value;

    /// <summary>
    /// The error, if present.
    /// </summary>
    private readonly Error _error;

    /// <summary>
    /// Indicates whether the result represents an error.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Creates a successful result with the given value.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    private Result(T value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }

    /// <summary>
    /// Creates an error result with the given error.
    /// </summary>
    /// <param name="error">The error to encapsulate.</param>
    private Result(Error error)
    {
        IsError = true;
        _value = default;
        _error = error;
    }

    public Result<R> Bind<R>(Func<T, Result<R>> bind)
    {
        return IsError ? Result<R>.Fail<R>(_error) : bind(_value);
    }

    /// <summary>
    /// Explicitly creates a new instance of a success Result
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <typeparam name="R">The type of the new Result</typeparam>
    /// <returns>A new instance of the Result.</returns>
    public static Result<R> Success<R>(R value) => new(value);

    /// <summary>
    /// Explicitly creates a new instance of a fail Result
    /// </summary>
    /// <typeparam name="R">The type of the new Result</typeparam>
    /// <returns>A new instance of the Result.</returns>
    public static Result<R> Fail<R>(Error error) => new(error);

    /// <summary>
    /// Allows implicit conversion from a successful value to a Result.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    /// <summary>
    /// Allows implicit conversion from an Error to a Result.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    /// <summary>
    /// Applies different functions depending on whether the result is successful or an error.
    /// </summary>
    /// <typeparam name="R">The return type of the functions.</typeparam>
    /// <param name="ok">The function to apply if the result is successful.</param>
    /// <param name="fail">The function to apply if the result is an error.</param>
    public R Match<R>(Func<T, R> ok, Func<Error, R> fail)
    {
        return IsError ? fail(_error) : ok(_value);
    }
}