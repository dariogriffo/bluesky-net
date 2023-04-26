namespace Bluesky.Net.Internals;

internal static class Constants
{
    internal const string BlueskyApiClient = "Bluesky";
    internal const string ContentMediaType = "application/json";
    internal const string AcceptedMediaType = "application/json";
    
    internal static class Urls
    {
        internal static class AtProtoServer
        {
            internal const string Login = "/xrpc/com.atproto.server.createSession";
            internal const string RefreshSession = "/xrpc/com.atproto.server.refreshSession";    
        }

        internal static class AtProtoIdentity
        {
            internal const string ResolveHandle = "/xrpc/com.atproto.identity.resolveHandle";
        }
    }

    internal class HeaderNames
    {
        internal const string UserAgent = "user-agent";
    }
}