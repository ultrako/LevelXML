using Xunit;

namespace LevelXML.Test;

public class DeleteShapesTest
{
    [Fact]
    public void DeleteShapesInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new DeleteShapes<Joint>());
    }
}