using Xunit;

namespace HappyWheels.Test;

public class SpecialActionsTest
{
    [Fact]
    public void TestImpulseSpecialDefaultValues()
    {
        ImpulseSpecial action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseSpecialHighValues()
    {
        ImpulseSpecial action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestChangeTextBoxOpacityDefaultValues()
    {
        ChangeTextBoxOpacity action = new();
        Assert.Equal(100, action.Opacity);
        Assert.Equal(1, action.Duration);
    }

    [Fact]
    public void TestChangeTextBoxOpacityHighValues()
    {
        ChangeTextBoxOpacity action = new(double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Opacity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestSlideTextBoxDefaultValues()
    {
        SlideTextBox action = new();
        Assert.Equal(1, action.Duration);
        Assert.Equal(0, action.X);
        Assert.Equal(0, action.Y);
    }

    [Fact]
    public void TestSlideTextBoxHighValues()
    {
        SlideTextBox action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestImpulseNPCDefaultValues()
    {
        ImpulseNPC action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseNPCHighValues()
    {
        ImpulseNPC action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }
}