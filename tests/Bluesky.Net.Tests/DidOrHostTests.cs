namespace Bluesky.Net.Tests;

using FluentAssertions;
using Xunit;

public class DidOrHostTests
{
    [Fact]
    public void Valid_Did()
    {
        var did = new Did("did:web:asdf.org");
        DidOrHost sut = did;
        Did back = sut;
        back.Should().BeEquivalentTo(did);
    }
    [Fact]
    public void Valid_Host()
    {
        var host = new Host("multi.part.domain");
        DidOrHost sut = host;
        Host back = sut;
        back.Should().BeEquivalentTo(host);
    }
}
