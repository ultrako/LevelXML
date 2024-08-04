using Xunit;
using System;

namespace LevelXML.Test;

public class RailTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Rail rail = new();
        Assert.Equal(0, rail.Rotation);
        Assert.Equal(250, rail.Width);
        Assert.Equal(18, rail.Height);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Rail rail = new();
        rail.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        Rail rail = new();
        rail.Width = double.NaN;
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        Rail rail = new();
        rail.Height = double.NaN;
    }
}