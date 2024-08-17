using Xunit;

namespace LevelXML.Test;

public class AwakeFromSleepTest
{
    [Fact]
    public void AwakeFromSleepInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new AwakeFromSleep<Joint>());
    }
}