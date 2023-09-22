using Xunit;

namespace HappyWheels.Test;

public class SpecialActionsTest
{
    [Fact]
    public void ImpulseInvalidSpecialType()
    {
        Assert.Throws<LevelXMLException>(() => new Impulse<Fan>());
    }
    
    [Fact]
    public void TestChangeTextBoxOpacityDefaultValues()
    {
        ChangeOpacity<TextBox> action = new();
        Assert.Equal(100, action.Opacity);
        Assert.Equal(1, action.Duration);
    }

    [Fact]
    public void TestChangeTextBoxOpacityHighValues()
    {
        ChangeOpacity<TextBox> action = new(double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Opacity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestSlideTextBoxDefaultValues()
    {
        Slide action = new();
        Assert.Equal(1, action.Duration);
        Assert.Equal(0, action.X);
        Assert.Equal(0, action.Y);
    }

    [Fact]
    public void TestSlideTextBoxHighValues()
    {
        Slide action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestImpulseNPCDefaultValues()
    {
        Impulse<NonPlayerCharacter> action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseNPCHighValues()
    {
        Impulse<NonPlayerCharacter> action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestImpulseGlassPanelDefaultValues()
    {
        Impulse<GlassPanel> action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseGlassPanelHighValues()
    {
        Impulse<GlassPanel> action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }
}