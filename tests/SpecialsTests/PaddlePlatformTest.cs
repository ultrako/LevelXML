using Xunit;

namespace LevelXML.Test;

public class PaddlePlatformTest
{
    [Fact]
    public void TestDefaultValues()
    {
        PaddlePlatform platform = new();
        Assert.Equal(0, platform.Rotation);
        Assert.Equal(0, platform.Delay);
        Assert.False(platform.Reverse);
        Assert.Equal(90, platform.MaxAngle);
        Assert.Equal(10, platform.Speed);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        PaddlePlatform platform = new();
        platform.Rotation = double.NaN;
    }
}