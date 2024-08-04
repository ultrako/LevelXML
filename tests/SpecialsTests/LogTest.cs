using Xunit;
using System;

namespace LevelXML.Test;

public class LogTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Log log = new();
        Assert.Equal(0, log.Rotation);
        Assert.Equal(36, log.Width);
        Assert.Equal(400, log.Height);
        Assert.False(log.Fixed);
        Assert.False(log.Sleeping);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Log log = new();
        log.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        Log log = new();
        log.Width = double.NaN;
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        Log log = new();
        log.Height = double.NaN;
    }
}