namespace Bluesky.Net;

using Commands;
using Models;
using Multiples;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
public interface IBlueskyApi
{
    /// <summary>
    /// Tries to create a session for the user in the <see cref="Login"/>
    /// </summary>
    /// <param name="command">The login details</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Multiple<Session, Error>> Login(Login command, CancellationToken cancellationToken);

    /// <summary>
    /// Tries to refresh the <see cref="Session"/>
    /// </summary>
    /// <param name="session">The session</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Multiple<Session, Error>> RefreshSession(Session session, CancellationToken cancellationToken);

    /// <summary>
    /// Tries to resolve the <see cref="handle"/> to a valid <see cref="Did"/>
    /// </summary>
    /// <param name="handle">The handle</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Multiple<Did?, Error>> ResolveHandle(string handle, CancellationToken cancellationToken);
}