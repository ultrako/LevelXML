using Xunit;
using System;

namespace HappyWheels.Test;

public class SignTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Sign sign = new();
        Assert.Equal(0, sign.Rotation);
        Assert.Equal(SignType.RightArrow, sign.SignType);
        Assert.True(sign.ShowSignPost);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Sign sign = new();
        Assert.Throws<LevelXMLException>(() => sign.Rotation = double.NaN);
    }
}