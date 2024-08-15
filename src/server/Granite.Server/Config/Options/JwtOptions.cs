namespace Granite.Server.Config.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }

    /// <summary>
    /// Amount of time, in minutes, that the token is valid.
    /// </summary>
    public double TokenExpiry { get; set; }

    /// <summary>
    /// Amount of time, in minutes, that the refresh token is valid.
    /// </summary>
    public double RefreshTokenExpiry { get; set; }
}