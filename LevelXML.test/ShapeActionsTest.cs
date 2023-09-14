using Xunit;
using System;

namespace HappyWheels.Test;

public class ShapeActionsTest
{
    [Fact]
    public void TestFixShape()
    {
        FixShape action = new();
        Assert.Equal(@"<a i=""1"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestNonfixShape()
    {
        NonfixShape action = new();
        Assert.Equal(@"<a i=""2"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeShapeOpacity()
    {
        ChangeShapeOpacity action = new();
        Assert.Equal(100, action.Opacity);
        Assert.Equal(1, action.Duration);
    }

    [Fact]
    public void TestImpulseShapeDefaultConstructor()
    {
        ImpulseShape action = new();
        Assert.Equal(10, action.X);
        Assert.Equal(-10, action.Y);
        Assert.Equal(0, action.Spin);
    }

    [Fact]
    public void TestImpulseShapeHighValues()
    {
        ImpulseShape action = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.X);
        Assert.Equal(double.PositiveInfinity, action.Y);
        Assert.Equal(double.PositiveInfinity, action.Spin);
    }

    [Fact]
    public void TestDeleteShape()
    {
        DeleteShape action = new();
        Assert.Equal(@"<a i=""5"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestDeleteSelfShape()
    {
        DeleteSelfShape action = new();
        Assert.Equal(@"<a i=""6"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeShapeCollisionXmlConstructor()
    {
        ChangeShapeCollision action = new(@"<a i=""7"" p0=""3"" />");
        Assert.Equal(Collision.Nothing, action.Collision);
    }

    [Fact]
    public void TestChangeShapeCollision()
    {
        ChangeShapeCollision action = new(Collision.Everything);
        Assert.Equal(Collision.Everything, action.Collision);
    }

    [Fact]
    public void TestChangeShapeCollisionLackOfCollision()
    {
        Assert.Throws<LevelXMLException>(() => new ChangeShapeCollision(@"<a i=""7"" />"));
    }

    [Fact]
    public void TestChangeShapeCollisionChangeCollision()
    {
        ChangeShapeCollision action = new(Collision.Everything);
        action.Collision = 3;
        Assert.Equal(Collision.Nothing, action.Collision);
    }
}