using Xunit;
using System;

namespace HappyWheels.Test;

public class BackgroundTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(Background.Blank != Background.City);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(Background.Blank.Equals(0));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((double.NaN).GetHashCode(), Background.Buggy.GetHashCode());
    }
}