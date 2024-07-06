using Xunit;

namespace LevelXML.Test;

public class BladeWeaponTest
{
    [Fact]
    public void TestDefaultValues()
    {
        BladeWeapon weapon = new();
        Assert.Equal(0, weapon.Rotation);
        Assert.False(weapon.Reverse);
        Assert.False(weapon.Sleeping);
        Assert.True(weapon.Interactive);
        Assert.Equal(WeaponType.Battleaxe, weapon.WeaponType);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        BladeWeapon weapon = new();
        Assert.Throws<LevelXMLException>(() => weapon.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingWeaponTypeAsNaN()
    {
        BladeWeapon weapon = new();
        Assert.Throws<LevelXMLException>(() => weapon.WeaponType = double.NaN);
    }
}