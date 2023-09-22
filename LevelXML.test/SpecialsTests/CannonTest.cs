using Xunit;

namespace HappyWheels.Test;

public class CannonTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Cannon cannon = new();
        Assert.Equal(0, cannon.Rotation);
        Assert.Equal(0, cannon.StartRotation);
        Assert.Equal(0, cannon.FiringRotation);
        Assert.Equal(CannonType.Circus, cannon.CannonType);
        Assert.Equal(1, cannon.Delay);
        Assert.Equal(1, cannon.MuzzleScale);
        Assert.Equal(5, cannon.Power);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Cannon cannon = new();
        Assert.Throws<LevelXMLException>(() => cannon.Rotation = double.NaN);
    }
}