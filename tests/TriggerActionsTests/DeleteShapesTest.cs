using Xunit;

namespace LevelXML.Test;

public class DeleteShapesTest
{
    [Fact]
    public void DeleteShapesInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new DeleteShapes<Joint>());
    }
}