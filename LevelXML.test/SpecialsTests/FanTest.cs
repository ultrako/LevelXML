using Xunit;
using System;

namespace HappyWheels.Test;

public class FanTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Fan fan = new();
        Assert.Equal(0, fan.Rotation);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Fan fan = new();
        Assert.Throws<LevelXMLException>(() => fan.Rotation = double.NaN);
    }
}