using System.Net;

namespace ErrorHandling;

public readonly struct Error
{
    public HttpStatusCode Code { get; }
    public string Message { get; }
    public string? Description { get; }

    private Error(HttpStatusCode code, string message, string description = "")
    {
        Code = code;
        Message = message;
        Description = description;
    }

    public static Error New(HttpStatusCode code, string message, string description = "")
    {
        return new Error(code, message, description);
    }

    public override string ToString()
    {
        return $"{Code}: {Message}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Error err && err.Code == Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(Error obj1, Error obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Error obj1, Error obj2)
    {
        return !obj1.Equals(obj2);
    }
}