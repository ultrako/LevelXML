using Xunit;

namespace LevelXML.Test;

public class NonfixTest
{
    [Fact]
    public void NonfixInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new Nonfix<Joint>());
    }
}