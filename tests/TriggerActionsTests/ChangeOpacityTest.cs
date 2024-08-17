using Xunit;

namespace LevelXML.Test;

public class ChangeOpacityTest
{
    [Fact]
    public void ChangeOpacityOfInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new ChangeOpacity<Joint>(100, 1));
    }
}