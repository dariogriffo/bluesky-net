namespace Bluesky.Net;

using Commands.AtProto.Server;
using Commands.Bsky.Feed;
using Models;
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
    Task<Result<Session>> Login(Login command, CancellationToken cancellationToken);

    /// <summary>
    /// Tries to refresh the <see cref="Session"/>
    /// </summary>
    /// <param name="session">The session</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<Session>> RefreshSession(Session session, CancellationToken cancellationToken);

    /// <summary>
    /// Get the profile of the actor with <see cref="did"/> 
    /// </summary>
    /// <param name="did">The id of the actor</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<Profile>> GetProfile(Did did, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get the profile of the actor with the current session 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<Profile>> GetProfile(CancellationToken cancellationToken);

    /// <summary>
    /// Tries to resolve the <see cref="handle"/> to a valid <see cref="Did"/>
    /// </summary>
    /// <param name="handle">The handle</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<Did>> ResolveHandle(string handle, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a post
    /// </summary>
    /// <param name="post"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<CreatePostResponse>> CreatePost(CreatePost post, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// Task<Result<AuthorFeed>> Query(GetAuthorFeed query, CancellationToken cancellationToken);
    ///
    
    
    
}
