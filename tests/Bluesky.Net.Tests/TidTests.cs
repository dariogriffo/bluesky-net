namespace Bluesky.Net.Tests;

using FluentAssertions;
using System;
using Xunit;

public class TidTests
{
    [Theory]
    [InlineData("3jg6anbimrc2a")]
    [InlineData("3yI5-c1z-cc2p-1a")]
    [InlineData("asdf234as4asdf234")]
    public void Valid_Tids(string tid)
    {
        var sut = new Tid(tid);
        sut.ToString().Should().Be(tid);
    }
    
    [Fact]
    public void Valid_Constructor()
    {
        var sut = new Tid(0,0);
        sut.ToString().Should().Be("2222-222-2222-22");
    }

    [Theory]
    [InlineData("")]
    [InlineData("com")]
    [InlineData("com.blah.Thing")]
    [InlineData("did:stuff:blah")]
    public void InValid_Tids(string tid)
    {
        Action action = () => new Tid(tid);
        action.Should().Throw<ArgumentException>();
    }
}
