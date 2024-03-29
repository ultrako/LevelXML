using Xunit;
using System;

namespace HappyWheels.Test;

public class SpringPlatformTest
{
    [Fact]
    public void TestDefaultValues()
    {
        SpringPlatform springPlatform = new();
        Assert.Equal(0, springPlatform.Rotation);
        Assert.Equal(0, springPlatform.Delay);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        SpringPlatform springPlatform = new();
        Assert.Throws<LevelXMLException>(() => springPlatform.Rotation = double.NaN);
    }
}