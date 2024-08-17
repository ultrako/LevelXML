using Xunit;

namespace LevelXML.Test;

public class NonfixTest
{
    [Fact]
    public void NonfixInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new Nonfix<Joint>());
    }
}