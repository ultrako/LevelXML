using Xunit;

namespace HappyWheels.Test;

public class ChangeCollisionTest
{
    [Fact]
    public void ChangeCollisionInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new ChangeCollision<Joint>(Collision.Everything));
    }
}