namespace Bluesky.Net.Internals;

using Microsoft.IdentityModel.Tokens;
using Models;
using Multiples;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

internal class SessionManager : IDisposable
{
    private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();

    private static readonly TokenValidationParameters DefaultTokenValidationParameters = new()
    {
        ValidateActor = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuer = false,
        ValidateSignatureLast = false,
        ValidateTokenReplay = false,
        ValidateIssuerSigningKey = false,
        ValidateWithLKG = false,
        LogValidationExceptions = false
    };

    private readonly BlueskyApiOptions _options;
    private Session? _session;
    private System.Timers.Timer? _timer;
    private int _refreshing;
    private readonly AtProtoServer _server;
    private bool _disposed;

    internal SessionManager(BlueskyApiOptions options, AtProtoServer server, Session session)
    {
        _options = options;
        _server = server;
        _server.TokenRefreshed += OnTokenRefreshed;
        _session = session;
    }

    private void OnTokenRefreshed(Session session)
    {
        SetSession(session);
    }

    internal void SetSession(Session session)
    {
        _session = session;
        if (!_options.AutoRenewSession)
        {
            return;
        }

        _timer ??= new Timer();

        ConfigureRefreshTokenTimer();
    }

    private void ConfigureRefreshTokenTimer()
    {
        Timer timer = _timer.ThrowIfNull();
        TimeSpan timeToNextRenewal = GetTimeToNextRenewal(_session.ThrowIfNull());
        timer.Elapsed += this.RefreshToken;
        timer.Interval = timeToNextRenewal.TotalSeconds;
        timer.Enabled = true;
        timer.Start();
    }

    private async void RefreshToken(object? sender, ElapsedEventArgs e)
    {
        if (Interlocked.Increment(ref _refreshing) > 1)
        {
            Interlocked.Decrement(ref _refreshing);
            return;
        }

        _timer.ThrowIfNull().Enabled = false;
        try
        {
            Multiple<Session, Error> result =
                await _server.ThrowIfNull().RefreshSession(_session.ThrowIfNull(), CancellationToken.None);

            result
                .Switch(s =>
                {
                    if (!_options.TrackSession)
                    {
                        return;
                    }

                    SetSession(s);
                }, _ => { });
        }
        finally
        {
            Interlocked.Decrement(ref _refreshing);
        }
    }

    private static TimeSpan GetTimeToNextRenewal(Session session)
    {
        JwtSecurityTokenHandler
            .ValidateToken(
                session.RefreshJwt,
                DefaultTokenValidationParameters,
                out SecurityToken token);
        return token.ValidTo.ToUniversalTime() - DateTime.UtcNow;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed || _timer is null)
        {
            return;
        }

        if (disposing)
        {
            _timer.Enabled = false;
            _timer.Dispose();
            _timer = null;
        }

        _disposed = true;
    }

    public void Dispose() => Dispose(true);
}
