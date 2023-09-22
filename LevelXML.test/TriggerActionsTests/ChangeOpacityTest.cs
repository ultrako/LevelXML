using Xunit;

namespace HappyWheels.Test;

public class ChangeOpacityTest
{
    [Fact]
    public void ChangeOpacityOfInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new ChangeOpacity<Joint>(100, 1));
    }
}