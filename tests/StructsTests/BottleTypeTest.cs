using Xunit;
using System;

namespace LevelXML.Test;

public class BottleTypeTest
{
    [Fact]
    public void TestEqualOperator()
    {
        BottleType bType = BottleType.Red;
        Assert.True(BottleType.Red == bType);
    }

    [Fact]
    public void TestNotEqualOperator()
    {
        BottleType bType = BottleType.Blue;
        Assert.True(BottleType.Yellow != bType);
    }

    [Fact]
    public void TestEquals()
    {
        BottleType bType = BottleType.Green;
        Assert.True(bType.Equals(BottleType.Green));
    }

    [Fact]
    public void TestNotEquals()
    {
        BottleType bType = BottleType.Green;
        Assert.False(bType.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        BottleType bType = BottleType.Green;
        Assert.Equal((1.0).GetHashCode(), bType.GetHashCode());
    }

    [Fact]
    public void BottleTypeAsNaN()
    {
        BottleType bType;
        Assert.Throws<LevelXMLException>(() => bType = double.NaN);
    }
}