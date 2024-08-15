using System.Net;
using ErrorHandling;

namespace Web;

public abstract class ApiResponse
{
    protected static class Statuses
    {
        public const string Success = "Success";
        public const string Fail = "Fail";
        public const string Exception = "Exception";
    }

    public string Status { get; init; }

    protected ApiResponse(string status)
    {
        Status = status;
    }

    /// <summary>
    /// Converts an object to an ApiResponse object.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static ApiResponse FromData<TValue>(TValue result)
    {
        return new ApiResponse<TValue>(data: result);
    }

    /// <summary>
    /// Converts a Result to an ApiResponse object.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static ApiResponse FromResult<TValue>(Result<TValue> result)
    {
        return result.Match<ApiResponse>(
            ok => new ApiResponse<TValue>(data: ok),
            err => new ApiResponseFail(data: err)
        );
    }

    /// <summary>
    /// Converts an Exception to an ApiResponse object.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static ApiResponse FromException(Exception e, HttpStatusCode code)
    {
        var err = Error.New(code, e.Message, $"{e.GetType().Name}:{e.GetHashCode()}");

        return new ApiResponseFail(err);

        // return new ApiResponseFail(message: e.Message, code: $"{e.GetType().Name}:{e.GetHashCode()}",
        //     data: new { e.Source, e.StackTrace });
    }

    /// <summary>
    /// Converts a List of objects into an ApiResponse object.
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ApiResponse FromList<T>(List<T> list)
    {
        return new ApiResponse<List<T>>(data: list);
    }
}

[Serializable]
public sealed class ApiResponse<T> : ApiResponse
{
    public T Data { get; init; }

    public ApiResponse(T data) : base(Statuses.Success) => Data = data;
}

[Serializable]
public sealed class ApiResponseFail : ApiResponse
{
    public ErrorData Data { get; init; }
    public ApiResponseFail(Error data) : base(Statuses.Fail) => Data = new ErrorData(data.Message, data.Description);
}

public sealed record ErrorData(string Message, string? Description = "");

// [Serializable]
// public sealed class ApiResponseException<T> : ApiResponse
// {
//     public string Message { get; init; }
//     public T? Data { get; init; }
//     public string? Code { get; set; }
//
//     internal ApiResponseException(string message, string? code = "", T? data = default) : base(
//         Statuses.Exception)
//     {
//         Message = message;
//         Data = data;
//         Code = code;
//     }
// }