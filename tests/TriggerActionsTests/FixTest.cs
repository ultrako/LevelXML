using Xunit;

namespace LevelXML.Test;

public class FixTest
{
    [Fact]
    public void FixInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new Fix<Joint>());
    }
}