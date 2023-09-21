using Xunit;
using System;

namespace HappyWheels.Test;

public class BottleTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Bottle bottle = new();
        Assert.Equal(0, bottle.Rotation);
        Assert.Equal(BottleType.Green, bottle.BottleType);
        Assert.False(bottle.Sleeping);
        Assert.True(bottle.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Bottle bottle = new();
        Assert.Throws<LevelXMLException>(() => bottle.Rotation = double.NaN);
    }
}