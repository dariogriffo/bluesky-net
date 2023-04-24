using System;
using FluentAssertions;
using Xunit;

namespace Bluesky.Net.Tests;

public class DidTests
{
    [Theory]
    [InlineData("did:web:asdf.org")]
    [InlineData("did:plc:asdf")]
    public void Valid_Dids(string did)
    {
        var sut = new Did(did);
        sut.ToString().Should().Be(did);
    }

    [Theory]
    [InlineData("bob.com")]
    [InlineData("")]
    [InlineData("did:")]
    [InlineData("did:plc:")]
    [InlineData("plc:asdf")]
    [InlineData("DID:thing:thang")]
    public void InValid_Dids(string did)
    {
        Action action = () => new Did(did);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Type_With_Valid_Value_Returns_Correct_Value()
    {
        const string type = "web";
        string did = $"did:{type}:asdf.org";
        var sut = new Did(did);
        sut.Type().Should().Be(type);
    }
}
