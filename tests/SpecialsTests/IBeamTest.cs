using Xunit;
using System;

namespace LevelXML.Test;

public class IBeamTest
{
    [Fact]
    public void TestDefaultValues()
    {
        IBeam iBeam = new();
        Assert.Equal(0, iBeam.Rotation);
        Assert.Equal(400, iBeam.Width);
        Assert.Equal(32, iBeam.Height);
        Assert.False(iBeam.Fixed);
        Assert.False(iBeam.Sleeping);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        IBeam iBeam = new();
        iBeam.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        IBeam iBeam = new();
        iBeam.Width = double.NaN;
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        IBeam iBeam = new();
        iBeam.Height = double.NaN;
    }
}