using Xunit;
using System;

namespace LevelXML.Test;

public class BoostTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Boost boost = new();
        Assert.Equal(0, boost.Rotation);
        Assert.Equal(2, boost.Panels);
        Assert.Equal(20, boost.Speed);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Boost boost = new();
        boost.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingPanelsAsNaN()
    {
        Boost boost = new();
        boost.Panels = double.NaN;
    }
}