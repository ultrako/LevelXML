using Xunit;

namespace LevelXML.Test;

public class VanTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Van van = new();
        Assert.Equal(0, van.Rotation);
        Assert.False(van.Sleeping);
        Assert.True(van.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Van van = new();
        van.Rotation = double.NaN;
    }
}