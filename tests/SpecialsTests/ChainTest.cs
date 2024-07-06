using Xunit;

namespace LevelXML.Test;

public class ChainTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Chain chain = new();
        Assert.Equal(0, chain.Rotation);
        Assert.False(chain.Sleeping);
        Assert.True(chain.Interactive);
        Assert.Equal(20, chain.LinkCount);
        Assert.Equal(1, chain.LinkScale);
        Assert.Equal(0, chain.Curve);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        Chain chain = new();
        Assert.Throws<LevelXMLException>(() => chain.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingLinkScaleAsNaN()
    {
        Chain chain = new();
        Assert.Throws<LevelXMLException>(() => chain.LinkScale = double.NaN);
    }

    [Fact]
    public void TestSettingCurveAsNaN()
    {
        Chain chain = new();
        Assert.Throws<LevelXMLException>(() => chain.Curve = double.NaN);
    }
}