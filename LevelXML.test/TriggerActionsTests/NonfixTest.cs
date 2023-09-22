using Xunit;

namespace HappyWheels.Test;

public class NonfixTest
{
    [Fact]
    public void NonfixInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new Nonfix<Joint>());
    }
}