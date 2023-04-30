using Bluesky.Net;
using Bluesky.Net.Commands.AtProto.Server;
using Bluesky.Net.Commands.Bsky.Feed;
using Bluesky.Net.Commands.Bsky.Feed.Model;
using Bluesky.Net.Json;
using Bluesky.Net.Models;
using Bluesky.Net.Queries.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

ServiceCollection services = new();
services.AddBluesky();
await using ServiceProvider sp = services.BuildServiceProvider();
JsonSerializerOptions printOptions =
    new()
    {
        WriteIndented = true,
        Converters =
        {
            new DidJsonConverter(), new AtUriJsonConverter(), new NsidJsonConverter(), new TidJsonConverter()
        },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

IBlueskyApi api = sp.GetRequiredService<IBlueskyApi>();

string userName = Environment.GetEnvironmentVariable("BLUESKY_USERNAME")!;
string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;
Login command = new(userName, password);
Result<Session> result = await api.Login(command, CancellationToken.None);
await result.SwitchAsync(async session =>
{
    Console.WriteLine("Logged in");
    Console.WriteLine(JsonSerializer.Serialize(session, printOptions));

    //Get the logged in user's profile
    Result<Profile> profileResult = await api.GetProfile(CancellationToken.None);
    
    profileResult.Switch(profile =>
    {
        Console.WriteLine("User profile");
        Console.WriteLine(JsonSerializer.Serialize(profile, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));

    
    //Resolve a user's DID
    Result<Did> resolvedHandle = await api.ResolveHandle(session.Handle, CancellationToken.None);

    resolvedHandle.Switch(handleResolved =>
    {
        Console.WriteLine("Handle resolved");
        Console.WriteLine(JsonSerializer.Serialize(handleResolved, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));

    //Refresh the token
    Result<Session> result1 = await api.RefreshSession(session, CancellationToken.None);
    result1.Switch(refresh =>
    {
        Console.WriteLine("Token refreshed");
        Console.WriteLine(JsonSerializer.Serialize(refresh, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));


    string text =
        @"Link to Google This post is created with Bluesky.Net. A library to interact with Bluesky. A mention to myself and an emoji '🌅'";
    int mentionStart = text.IndexOf("myself", StringComparison.InvariantCulture);
    int mentionEnd = mentionStart + Encoding.Default.GetBytes("myself").Length;
    CreatePost post = new(
        text,
        new Link("www.google.com", 0, "Link to Google".Length),
        new Mention(session.Did, mentionStart, mentionEnd));

    //Create a post
    Result<CreatePostResponse> created = await api.CreatePost(post, CancellationToken.None);

    created.Switch(x =>
    {
        Console.WriteLine(JsonSerializer.Serialize(x, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));
}, _ =>
{
    Console.WriteLine(JsonSerializer.Serialize(_, printOptions));
    return Task.CompletedTask;
});
