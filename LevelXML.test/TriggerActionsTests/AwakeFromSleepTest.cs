using Xunit;

namespace HappyWheels.Test;

public class AwakeFromSleepTest
{
    [Fact]
    public void AwakeFromSleepInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new AwakeFromSleep<Joint>());
    }
}