using Xunit;
using System;

namespace LevelXML.Test;

public class JetTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Jet jet = new();
        Assert.Equal(0, jet.Rotation);
        Assert.False(jet.Sleeping);
        Assert.Equal(1, jet.Power);
        Assert.Equal(0, jet.FiringTime);
        Assert.Equal(0, jet.AccelerationTime);
        Assert.False(jet.FixedAngle);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Jet jet = new();
        jet.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingPowerAsNaN()
    {
        Jet jet = new();
        jet.Power = double.NaN;
    }
}