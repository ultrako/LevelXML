using Xunit;

namespace LevelXML.Test;

public class ChangeCollisionTest
{
    [Fact]
    public void ChangeCollisionInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new ChangeCollision<Joint>(Collision.Everything));
    }
}