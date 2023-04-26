namespace Bluesky.Net.Models;

using System.Text.Json.Serialization;

public class Session
{
    [JsonConstructor]
    public Session(
        Did did,
        string handle,
        string email,
        string accessJwt,
        string refreshJwt
    )
    {
        Did = did;
        Handle = handle;
        Email = email;
        AccessJwt = accessJwt;
        RefreshJwt = refreshJwt;
    }

    public Did Did { get; }

    public string Handle { get; }

    public string Email { get; }

    public string AccessJwt { get; }

    public string RefreshJwt { get; }    
}
