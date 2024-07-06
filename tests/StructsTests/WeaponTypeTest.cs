using Xunit;
using System;

namespace LevelXML.Test;

public class WeaponTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(WeaponType.Axe != WeaponType.Katana);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(WeaponType.Battleaxe.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((4.0).GetHashCode(), WeaponType.Cleaver.GetHashCode());
    }
}