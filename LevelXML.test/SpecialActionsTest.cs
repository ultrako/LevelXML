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
}