using Xunit;
using System;

namespace HappyWheels.Test;

public class HarpoonTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Harpoon harpoon = new();
        Assert.Equal(0, harpoon.Rotation);
        Assert.True(harpoon.Anchor);
        Assert.False(harpoon.FixedTurret);
        Assert.Equal(0, harpoon.TurretAngle);
        Assert.False(harpoon.TriggerFiring);
        Assert.False(harpoon.StartDeactivated);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Harpoon harpoon = new();
        Assert.Throws<LevelXMLException>(() => harpoon.Rotation = double.NaN);
    }
}