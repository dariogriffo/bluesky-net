namespace Bluesky.Net.Internals;

using Commands.Bsky.Feed;
using Model;
using Models;
using Multiples;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class AtProtoRepo
{
    private readonly HttpClient _client;

    public AtProtoRepo(HttpClient client)
    {
        _client = client;
    }

    public Task<Multiple<CreatePostResponse, Error>> Create(CreateRecord record, CancellationToken cancellationToken)
    {
        return
            _client
                .Post<CreateRecord, CreatePostResponse>(
                    Constants.Urls.AtProtoRepo.CreateRecord, record,
                    cancellationToken);
    }
}
