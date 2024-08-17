using Xunit;

namespace LevelXML.Test;

public class DeleteSelfTest
{
    [Fact]
    public void DeleteSelfInvalidEntity()
    {
        Assert.Throws<LevelInvalidException>(() => new DeleteSelf<Joint>());
    }
}