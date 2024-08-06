using Xunit;

namespace LevelXML.Test;

public class ArrowGunTest
{
    [Fact]
    public void TestDefaultValues()
    {
        ArrowGun arrowGun = new();
        Assert.Equal(0, arrowGun.Rotation);
        Assert.False(arrowGun.Fixed);
        Assert.Equal(5, arrowGun.RateOfFire);
        Assert.True(arrowGun.ShootPlayer);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        ArrowGun arrowGun = new();
        arrowGun.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingFixedAsTrue()
    {
        ArrowGun arrowGun = new();
        arrowGun.Fixed = true;
        Assert.Equal("<sp t=\"29\" p0=\"0\" p1=\"0\" p2=\"0\" p3=\"t\" p4=\"5\" p5=\"f\" />", arrowGun.ToXML());
    }
}