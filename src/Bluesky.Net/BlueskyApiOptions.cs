namespace Bluesky.Net;

public class BlueskyApiOptions
{
    public string Url { get; set; } = "https://bsky.social";
    
    public string UserAgent { get; set; } = "Bluesky.Net";

    public bool TrackSession { get; set; } = true;

    public bool AutoRenewSession { get; set; } = false;
}
