using Xunit;
using System;

namespace LevelXML.Test;

public class BoomboxTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Boombox boombox = new();
        Assert.Equal(0, boombox.Rotation);
        Assert.False(boombox.Sleeping);
        Assert.True(boombox.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Boombox boombox = new();
        boombox.Rotation = double.NaN;
    }
}