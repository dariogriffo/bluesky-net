namespace Bluesky.Net.Internals;

using Commands.AtProto.Server;
using Commands.Bsky.Feed;
using Model;
using Models;
using Multiples;
using Queries.Feed;
using Queries.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

internal class BlueskyApi : IBlueskyApi, IDisposable
{
    private readonly BlueskyApiOptions _options;
    private readonly AtProtoServer _server;
    private readonly HttpClient _client;
    private readonly AtProtoIdentity _identity;
    private readonly BlueskyFeed _feed;
    private readonly AtProtoRepo _repo;
    private SessionManager? _sessionManager;

    public BlueskyApi(IHttpClientFactory factory, BlueskyApiOptions options)
    {
        _options = options;
        _client = factory.CreateClient(Constants.BlueskyApiClient);
        _server = new AtProtoServer(factory, _options);
        _identity = new AtProtoIdentity(_client);
        _repo = new AtProtoRepo(_client);
        _server.UserLoggedIn += OnUserLoggedIn;
        _server.TokenRefreshed += UpdateBearerToken;
    }

    public Task<Multiple<Session, Error>> Login(Login command, CancellationToken cancellationToken)
    {
        return _server.Login(command, cancellationToken);
    }

    public Task<Multiple<Session, Error>> RefreshSession(
        Session session,
        CancellationToken cancellationToken) => _server.RefreshSession(session, cancellationToken);

    public Task<Multiple<Did, Error>> ResolveHandle(
        string handle,
        CancellationToken cancellationToken) => _identity.ResolveHandle(handle, cancellationToken);

    public Task<Multiple<CreatePostResponse, Error>> CreatePost(
        CreatePost command,
        CancellationToken cancellationToken)
    {
        CreateRecord record = new(
            "app.bsky.feed.post",
            _sessionManager!.Session!.Did.ToString()!,
            new Record()
            {
                Text = command.Text, 
                Type = "app.bsky.feed.post",
                CreatedAt = DateTime.UtcNow,
                Facets = command.Facets
            });

        return _repo.Create(record, cancellationToken);
    }

    public Task<Multiple<AuthorFeed, Error>> Query(GetAuthorFeed query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _sessionManager?.Dispose();
    }

    private void UpdateBearerToken(Session session)
    {
        _client
                .DefaultRequestHeaders
                .Authorization =
            new AuthenticationHeaderValue("Bearer", session.AccessJwt);
    }

    private void OnUserLoggedIn(Session session)
    {
        UpdateBearerToken(session);

        if (!_options.TrackSession)
        {
            return;
        }

        if (_sessionManager is null)
        {
            _sessionManager = new SessionManager(_options, _server, session);
        }
        else
        {
            _sessionManager.SetSession(session);
        }
    }
}
