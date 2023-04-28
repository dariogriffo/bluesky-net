[![NuGet](https://img.shields.io/nuget/v/Bluesky.Net.svg?style=flat)](https://www.nuget.org/packages/Bluesky.Net/)
[![GitHub license](https://img.shields.io/github/license/dariogriffo/bluesky-net.svg)](https://raw.githubusercontent.com/dariogriffo/bluesky-net/main/LICENSE)

[![N|Solid](https://avatars2.githubusercontent.com/u/39886363?s=200&v=4)](https://github.com/dariogriffo/bluesky-net)


# Bluesky.Net
Unofficial .NET implementation of atproto.com

# About the target framework

To simplify the development, first versions will target only NET7.0+

Eventually the library will support NET standard 2.0

# About the results

The api implements a basic discriminated union pattern for all the results.

The first option of the result is what you expect, the second an error if the HTTP status code is > 2xx

# How to use it

1- Install the nuget package

`Install-Package Bluesky.Net`


2- Add the SDK to your services collection

```csharp
services.AddBluesky();
```

3- The SDK can be configured

```csharp
services
    .AddBluesky(options => {
        options.TrackSession = false;
    });
```

4- Inject the IBlueskyApi in your code

```csharp
public class MyClass
{
    private readonly IBlueskyApi _bluesky;
    public MyClass(IBlueskyApi bluesky)
    {
        _bluesky = bluesky;
    } 
}
```

5 - Call the api
```csharp
Login command = new("YOUR_HANDLE", "YOUR_PASSWORD");
Multiple<Session, Error> result = await _bluesky.Login(command, CancellationToken.None);

result
    .Switch(
        session => Console.WriteLine(session.Email),
        error => Console.WriteLine(JsonSerializer.Serialize(error)
    );    
```
6 - Create a post
```csharp

Multiple<Did, Error> resolvedHandle = await api.ResolveHandle(session.Handle, CancellationToken.None);
        
string text =
        @"Link to Google This post is created with Bluesky.Net. A library to interact with Bluesky. A mention to myself and an emoji '🌅'";
    int mentionStart = text.IndexOf("myself", StringComparison.InvariantCulture);
    int mentionEnd = mentionStart + Encoding.Default.GetBytes("myself").Length;
    CreatePost post = new(
        text,
        new Link("www.google.com", 0, "Link to Google".Length),
        new Mention(resolvedHandle.AsT0, mentionStart, mentionEnd));

    //Create a post
    Multiple<CreatePostResponse, Error> created = await api.CreatePost(post, CancellationToken.None);    
```
# Retries

You can easily plug retries with [Polly](https://github.com/App-vNext/Polly) when registering the api.
Install the nuget package

`Install-Package Microsoft.Extensions.Http.Polly`

Configure your retries:
```csharp
services
  .AddBluesky()
  .AddPolicyHandler(
      HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt))));
```