using Xunit;
using System;

namespace HappyWheels.Test;

public class TriggeredByTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(TriggeredBy.MainCharacter != TriggeredBy.AnyCharacter);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(TriggeredBy.MainCharacter.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((double.NaN).GetHashCode(), TriggeredBy.Nothing.GetHashCode());
    }
}