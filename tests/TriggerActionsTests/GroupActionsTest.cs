using Xunit;

namespace LevelXML.Test;

public class GroupActionsTest
{
    [Fact]
    public void TestAwakeGroupFromSleep()
    {
        AwakeFromSleep<Group> action = new();
        Assert.Equal(@"<a i=""0"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupOpacityDefaultValues()
    {
        ChangeOpacity<Group> action = new();
        Assert.Equal(@"<a i=""1"" p0=""100"" p1=""1"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupOpacityHighValues()
    {
        ChangeOpacity<Group> action = new(double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Opacity);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestFixGroup()
    {
        Fix<Group> action = new();
        Assert.Equal(@"<a i=""2"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestNonfixGroup()
    {
        Nonfix<Group> action = new();
        Assert.Equal(@"<a i=""3"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestImpulseGroupDefaultValues()
    {
        Impulse<Group> action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseGroupHighValues()
    {
        Impulse<Group> action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestDeleteShapeGroup()
    {
        DeleteShapes<Group> action = new();
        Assert.Equal(@"<a i=""5"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestDeleteSelfGroup()
    {
        DeleteSelf<Group> action = new();
        Assert.Equal(@"<a i=""6"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeGroupCollisionOneParamConstructor()
    {
        ChangeCollision<Group> action = new(Collision.Nothing);
        Assert.Equal(Collision.Nothing, action.Collision);
    }
}