namespace Bluesky.Net.Models;

using System.Text.Json.Serialization;

public class HandleResolution
{
    [JsonConstructor]
    public HandleResolution(Did did)
    {
        Did = did;
    }
    
    public Did Did { get; }
}
