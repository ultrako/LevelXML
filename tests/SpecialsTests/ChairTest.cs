using Xunit;
using System;

namespace LevelXML.Test;

public class ChairTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Chair chair = new();
        Assert.Equal(0, chair.Rotation);
        Assert.False(chair.Reverse);
        Assert.False(chair.Sleeping);
        Assert.True(chair.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Chair chair = new();
        chair.Rotation = double.NaN;
    }
}