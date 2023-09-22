using Xunit;
using System;

namespace HappyWheels.Test;

public class TelevisionTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Television television = new();
        Assert.Equal(0, television.Rotation);
        Assert.False(television.Sleeping);
        Assert.True(television.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Television television = new();
        Assert.Throws<LevelXMLException>(() => television.Rotation = double.NaN);
    }
}