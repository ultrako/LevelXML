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
}