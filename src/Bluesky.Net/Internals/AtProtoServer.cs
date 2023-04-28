namespace Bluesky.Net.Internals;

using Commands.AtProto.Server;
using Models;
using Multiples;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

internal delegate void UserLoggedIn(Session s);

internal delegate void TokenRefreshed(Session s);

internal class AtProtoServer
{
    private readonly BlueskyApiOptions _options;
    private readonly IHttpClientFactory _factory;

    internal AtProtoServer(IHttpClientFactory factory, BlueskyApiOptions options)
    {
        _factory = factory;
        _options = options;
    }

    internal async Task<Multiple<Session, Error>> Login(Login command, CancellationToken cancellationToken)
    {
        using HttpClient client = _factory.CreateClient(Constants.BlueskyApiClient);
        Multiple<Session, Error> result =
            await client.Post<Login, Session>(Constants.Urls.AtProtoServer.Login, command, cancellationToken);
        return
            result
                .Match(s =>
                {
                    if (!_options.TrackSession)
                    {
                        return result;
                    }

                    UserLoggedIn?.Invoke(s);

                    return result;
                }, error => error!);
    }

    public async Task<Multiple<Session, Error>> RefreshSession(
        Session session,
        CancellationToken cancellationToken)
    {
        using HttpClient client = _factory.CreateClient(Constants.BlueskyApiClient);
        client
            .DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

        var result = await client.Post<Session>(Constants.Urls.AtProtoServer.RefreshSession, cancellationToken);
        return
            result
                .Match(s =>
                {
                    if (!_options.TrackSession)
                    {
                        return result;
                    }

                    TokenRefreshed?.Invoke(s);
                    return result;
                }, error => error!);
    }

    internal UserLoggedIn? UserLoggedIn { get; set; }

    internal TokenRefreshed? TokenRefreshed { get; set; }
}
