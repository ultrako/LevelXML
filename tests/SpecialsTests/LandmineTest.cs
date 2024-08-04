using Xunit;
using System;

namespace LevelXML.Test;

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
        landmine.Rotation = double.NaN;
    }
}