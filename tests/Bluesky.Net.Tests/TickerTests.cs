namespace Bluesky.Net.Tests;

using FluentAssertions;
using Models;
using System;
using Xunit;

public class TickerTests
{
    [Fact]
    public void Iteration_Generate_Different_Values()
    {
        Ticker ticker = new();
        Tid prev = ticker.NextTid();
        Tid next = ticker.NextTid();
        for (int i = 0; i < 100; ++i)
        {
            String
                .Compare(next.ToString(), prev.ToString(), StringComparison.Ordinal)
                .Should()
                .BePositive();

            prev = next;
            next = ticker.NextTid();
        }

        next
            .ToString()
            .Substring(13, 3)
            .Should()
            .Be(prev.ToString().Substring(13, 3));

        Ticker otherTicker = new();
        Tid other = otherTicker.NextTid();

        String
            .Compare(other.ToString(), prev.ToString(), StringComparison.Ordinal)
            .Should()
            .BePositive();

        other
            .ToString()
            .Substring(13, 3)
            .Should()
            .NotBe(next.ToString().Substring(13, 3));

    }
}
