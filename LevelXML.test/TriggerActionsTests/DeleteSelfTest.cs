using Xunit;

namespace HappyWheels.Test;

public class DeleteSelfTest
{
    [Fact]
    public void DeleteSelfInvalidEntity()
    {
        Assert.Throws<LevelXMLException>(() => new DeleteSelf<Joint>());
    }
}