using Xunit;
using System;

namespace LevelXML.Test;

public class ToiletTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Toilet toilet = new();
        Assert.Equal(0, toilet.Rotation);
        Assert.False(toilet.Reverse);
        Assert.False(toilet.Sleeping);
        Assert.True(toilet.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Toilet toilet = new();
        toilet.Rotation = double.NaN;
    }
}