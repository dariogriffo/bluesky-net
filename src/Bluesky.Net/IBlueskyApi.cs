namespace Bluesky.Net;

using Commands;
using Commands.AtProto.Server;
using Commands.Bsky.Feed;
using Models;
using Multiples;
using Queries;
using Queries.Feed;
using Queries.Model;
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
    
    /// <summary>
    /// Creates a post
    /// </summary>
    /// <param name="post"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Multiple<CreatePostResponse, Error>> CreatePost(CreatePost post, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// Task<Multiple<AuthorFeed, Error>> Query(GetAuthorFeed query, CancellationToken cancellationToken);
    ///
    
    
    
}
