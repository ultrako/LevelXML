using Xunit;
using System;

namespace LevelXML.Test;

public class CannonTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(CannonType.Circus != CannonType.Metal);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(CannonType.Circus.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((double.NaN).GetHashCode(), CannonType.Invisible.GetHashCode());
    }
}