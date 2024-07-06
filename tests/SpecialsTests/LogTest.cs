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
        Assert.Throws<LevelXMLException>(() => log.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        Log log = new();
        Assert.Throws<LevelXMLException>(() => log.Width = double.NaN);
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        Log log = new();
        Assert.Throws<LevelXMLException>(() => log.Height = double.NaN);
    }
}