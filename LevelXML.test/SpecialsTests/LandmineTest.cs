using Xunit;
using System;

namespace HappyWheels.Test;

public class LandmineTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Landmine landmine = new();
        Assert.Equal(0, landmine.Rotation);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Landmine landmine = new();
        Assert.Throws<LevelXMLException>(() => landmine.Rotation = double.NaN);
    }
}