using Xunit;
using System;

namespace LevelXML.Test;
public class CollisionTest
{
    [Fact]
    public void CollisionNotEquals()
    {
        bool b = Collision.NaN != Collision.OnlyCharacters;
        Assert.True(b);
    }

    [Fact]
    public void CollisionEqualsDouble()
    {
        bool b = Collision.Everything.Equals(1);
        Assert.False(b);
    }

    [Fact]
    public void CollisionGetHashCode()
    {
        Assert.Equal((4.0).GetHashCode(), Collision.EverythingButSame.GetHashCode());
    }
}