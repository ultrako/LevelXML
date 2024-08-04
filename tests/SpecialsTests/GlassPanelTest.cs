using Xunit;
using System;

namespace LevelXML.Test;

public class GlassPanelTest
{
    [Fact]
    public void TestDefaultValues()
    {
        GlassPanel glassPanel = new();
        Assert.Equal(10, glassPanel.Width);
        Assert.Equal(100, glassPanel.Height);
        Assert.Equal(0, glassPanel.Rotation);
        Assert.False(glassPanel.Sleeping);
        Assert.Equal(10, glassPanel.Strength);
        Assert.True(glassPanel.Stabbing);
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        GlassPanel glassPanel = new();
        glassPanel.Width = double.NaN;
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        GlassPanel glassPanel = new();
        glassPanel.Height = double.NaN;
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        GlassPanel glassPanel = new();
        glassPanel.Rotation = double.NaN;
    }
}