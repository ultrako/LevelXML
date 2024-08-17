using Xunit;

namespace LevelXML.Test;

public class ShapeActionsTest
{
    [Fact]
    public void TestFixShape()
    {
        Fix<Shape> action = new();
        Assert.Equal(@"<a i=""1"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestNonfixShape()
    {
        Nonfix<Shape> action = new();
        Assert.Equal(@"<a i=""2"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeShapeOpacity()
    {
        ChangeOpacity<Shape> action = new();
        Assert.Equal(100, action.Opacity);
        Assert.Equal(1, action.Duration);
    }

    [Fact]
    public void TestImpulseShapeDefaultConstructor()
    {
        Impulse<Shape> action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseShapeHighValues()
    {
        Impulse<Shape> action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestDeleteShape()
    {
        DeleteShapes<Shape> action = new();
        Assert.Equal(@"<a i=""5"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestDeleteSelfShape()
    {
        DeleteSelf<Shape> action = new();
        Assert.Equal(@"<a i=""6"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeShapeCollisionXmlConstructor()
    {
        ChangeCollision<Shape> action = new(@"<a i=""7"" p0=""3"" />");
        Assert.Equal(Collision.Nothing, action.Collision);
    }

    [Fact]
    public void TestChangeShapeCollision()
    {
        ChangeCollision<Shape> action = new(Collision.Everything);
        Assert.Equal(Collision.Everything, action.Collision);
    }

    [Fact]
    public void TestChangeShapeCollisionLackOfCollision()
    {
        ChangeCollision<Shape> action = new(@"<a i=""7"" />");
        Assert.Equal(Collision.Everything, action.Collision);
    }

    [Fact]
    public void TestChangeShapeCollisionChangeCollision()
    {
        ChangeCollision<Shape> action = new(Collision.Everything);
        action.Collision = 3;
        Assert.Equal(Collision.Nothing, action.Collision);
    }
}