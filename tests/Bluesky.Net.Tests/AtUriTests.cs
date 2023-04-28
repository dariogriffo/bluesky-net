namespace Bluesky.Net.Tests;

using Commands.Bsky.Feed;
using FluentAssertions;
using Models;
using System.Text.Json;
using Xunit;

public class AtUriTests
{
    [Theory]
    [InlineData("at://bob.com")]
    [InlineData("at://bob.com/")]
    [InlineData("at://did:plc:bv6ggog3tya2z3vxsub7hnal")]
    [InlineData("at://bob.com/io.example.song")]
    [InlineData("at://bob.com/io.example.song/")]
    [InlineData("at://bob.com/io.example.song/3yI5-c1z-cc2p-1a")]
    [InlineData("at://bob.com/io.example.song/3yI5-c1z-cc2p-1a#/title")]
    [InlineData("at://did:plc:ltk4reuh7rkoy2frnueetpb5/app.bsky.follow/3jg23pbmlhc2a")]
    [InlineData("at://did:plc:hdfawlopxvzukvkxn3z5lgko/app.bsky.feed.post/3juegsobnjf2n")]
    public void Valid_AtUri(string uri)
    {
        AtUri sut = new AtUri(uri);
        sut.ToString().Should().Be(uri);
    }

    [Fact]
    public void Valid_Collection()
    {
        const string record = "3yI5-c1z-cc2p-1a";
        const string fragment = "/title";
        const string repository = "bob.com";
        const string collection = "io.example.song";
        AtUri sut = new AtUri($"at://{repository}/{collection}/{record}#{fragment}");
        sut.Collection.Should().BeEquivalentTo(collection);
    }

    [Fact]
    public void Valid_Record()
    {
        const string record = "3yI5-c1z-cc2p-1a";
        const string fragment = "/title";
        const string repository = "bob.com";
        const string collection = "io.example.song";
        AtUri sut = new AtUri($"at://{repository}/{collection}/{record}#{fragment}");
        sut.Record.Should().BeEquivalentTo(record);
    }

    [Fact]
    public void Valid_Fragment()
    {
        const string record = "3yI5-c1z-cc2p-1a";
        const string fragment = "/title";
        const string repository = "bob.com";
        const string collection = "io.example.song";
        AtUri sut = new AtUri($"at://{repository}/{collection}/{record}#{fragment}");
        sut.Fragment.Should().BeEquivalentTo(fragment);
    }

    [Fact]
    public void Valid_Repository()
    {
        const string record = "3yI5-c1z-cc2p-1a";
        const string fragment = "/title";
        DidOrHost repository = new("bob.com");
        const string collection = "io.example.song";
        AtUri sut = new AtUri($"at://{repository}/{collection}/{record}#{fragment}");
        sut.Repository.ToString().Should().BeEquivalentTo(repository.ToString());
    }
}
