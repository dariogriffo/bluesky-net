using System;
using FluentAssertions;
using Xunit;

namespace Bluesky.Net.Tests;

using Models;

public class DidTests
{
    [Theory]
    [InlineData("did:web:asdf.org")]
    [InlineData("did:plc:asdf")]
    public void Valid_Dids(string? did)
    {
        Did sut = new Did(did);
        sut.ToString().Should().Be(did);
    }

    [Theory]
    [InlineData(
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6cGxjOmV4M3NpNTI3Y2QyYW9nYnZpZGtvb296YyIsImlhdCI6MTY2NjgyOTM5M30=.UvZgTqvaJICONa1wIUT1bny7u3hqVAqWhWy3qeuyZrE",
        "did:plc:ex3si527cd2aogbvidkooozc")]
    [InlineData(
        "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCIsInVjdiI6IjAuOS4wLWNhbmFyeSJ9.eyJhdHQiOltdLCJhdWQiOiJkaWQ6cGxjOnM3b25ieWphN2MzeXJzZ3Zob2xrbHM1YiIsImV4cCI6MTY3NTM4Mzg2NywiZmN0IjpbXSwiaXNzIjoiZGlkOmtleTp6RG5hZWRHVGJkb0Frb1NlOG96a3k1WHAzMjZTVFpUSm50aDlHY2dxaTZQYjNzYjczIiwibm5jIjoiTnZURDhENWZjNXFpalIyMWJ1V2Z1ZE02dzlBM2drSy1ac3RtUW03b21pdyIsInByZiI6W119.QwZkb9R17tNhXnY_roqFYgdiIgUnSC18FYWQb3PcH6BU1R5l4W_T4XdACyczPGfM-jAnF2r2loBXDntYVS6N5A",
        "did:plc:s7onbyja7c3yrsgvholkls5b")]
    public void Jwt_Dids(string? token, string did)
    {
        Did sut = new Did(token);
        sut.ToString().Should().Be(did);
    }

    [Theory]
    [InlineData("bob.com")]
    [InlineData("")]
    [InlineData("did:")]
    [InlineData("did:plc:")]
    [InlineData("plc:asdf")]
    [InlineData("DID:thing:thang")]
    public void InValid_Dids(string? did)
    {
        Action action = () => new Did(did);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Type_With_Valid_Value_Returns_Correct_Value()
    {
        const string type = "web";
        string? did = $"did:{type}:asdf.org";
        Did sut = new Did(did);
        sut.Type.Should().Be(type);
    }
}
