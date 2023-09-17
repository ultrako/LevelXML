using Xunit;

namespace HappyWheels.Test;

public class GroupActionsTest
{
    [Fact]
    public void TestAwakeGroupFromSleep()
    {
        AwakeGroupFromSleep action = new();
        Assert.Equal(@"<a i=""0"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupOpacityDefaultValues()
    {
        ChangeGroupOpacity action = new();
        Assert.Equal(@"<a i=""1"" p0=""100"" p1=""1"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupOpacityHighValues()
    {
        ChangeGroupOpacity action = new(999999, double.PositiveInfinity);
        Assert.Equal(100, action.Opacity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestFixGroup()
    {
        FixGroup action = new();
        Assert.Equal(@"<a i=""2"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestNonfixGroup()
    {
        NonfixGroup action = new();
        Assert.Equal(@"<a i=""3"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestImpulseGroupDefaultValues()
    {
        ImpulseGroup action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseGroupHighValues()
    {
        ImpulseGroup action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestDeleteShapeGroup()
    {
        DeleteShapeGroup action = new();
        Assert.Equal(@"<a i=""5"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestDeleteSelfGroup()
    {
        DeleteSelfGroup action = new();
        Assert.Equal(@"<a i=""6"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupCollisionDefaultValue()
    {
        ChangeGroupCollision action = new();
        Assert.Equal(Collision.Everything, action.Collision);
    }

    [Fact]
    public void TestChangeGroupCollisionOneParamConstructor()
    {
        ChangeGroupCollision action = new(Collision.Nothing);
        Assert.Equal(Collision.Nothing, action.Collision);
    }
}