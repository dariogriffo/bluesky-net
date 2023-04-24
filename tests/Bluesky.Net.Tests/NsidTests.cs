namespace Bluesky.Net.Tests;

using FluentAssertions;
using Xunit;

public class NsidTests
{
    [Fact]
    public void Valid_Nsid()
    {
        const string nsid = "com.atproto.recordType";
        var sut = new Nsid(nsid);
        sut.ToString().Should().Be(nsid);
    }
    
    [Fact]
    public void Valid_Domain()
    {
        const string domain = "com.atproto";
        const string name = "recordType";
        string nsid = $"{domain}.{name}";
        var sut = new Nsid(nsid);
        sut.Domain.Should().Be("atproto.com");
    }
    
    
    [Fact]
    public void Valid_Name()
    {
        const string domain = "com.atproto";
        const string name = "recordType";
        string nsid = $"{domain}.{name}";
        var sut = new Nsid(nsid);
        sut.Name.Should().Be(name);
    }
}
