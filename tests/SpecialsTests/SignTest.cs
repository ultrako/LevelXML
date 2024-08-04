using Xunit;
using System;

namespace LevelXML.Test;

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
        sign.Rotation = double.NaN;
    }
}