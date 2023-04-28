using Bluesky.Net;
using Bluesky.Net.Commands;
using Bluesky.Net.Commands.AtProto.Server;
using Bluesky.Net.Commands.Bsky.Feed;
using Bluesky.Net.Commands.Bsky.Feed.Model;
using Bluesky.Net.Json;
using Bluesky.Net.Models;
using Bluesky.Net.Multiples;
using Microsoft.Extensions.DependencyInjection;
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
    };
IBlueskyApi api = sp.GetRequiredService<IBlueskyApi>();

Login command = new("", "");
Multiple<Session, Error> result = await api.Login(command, CancellationToken.None);
await result.SwitchAsync(async session =>
{
    CreatePost post = new(
        @"This post is created with Bluesky.Net. A library to interact with Bluesky.
'🌅'");
    Multiple<CreatePostResponse, Error> created = await api.CreatePost(post, CancellationToken.None);

    created.Switch(x =>
    {
        Console.WriteLine(JsonSerializer.Serialize(x, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));

    Console.WriteLine("Logged in");
    Console.WriteLine(JsonSerializer.Serialize(session, printOptions));
    await Task.Delay(TimeSpan.FromSeconds(1));
    Multiple<Did?, Error> resolvedHandle = await api.ResolveHandle(session.Handle, CancellationToken.None);

    resolvedHandle.Switch(handleResolved =>
    {
        Console.WriteLine("Handle resolved");
        Console.WriteLine(JsonSerializer.Serialize(handleResolved, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));

    Multiple<Session, Error> result1 = await api.RefreshSession(session, CancellationToken.None);
    result1.Switch(refresh =>
    {
        Console.WriteLine("Token refreshed");
        Console.WriteLine(JsonSerializer.Serialize(refresh, printOptions));
    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, printOptions)));
}, _ =>
{
    Console.WriteLine(JsonSerializer.Serialize(_, printOptions));
    return Task.CompletedTask;
});
