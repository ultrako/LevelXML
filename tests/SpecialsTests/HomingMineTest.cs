using Xunit;
using System;

namespace LevelXML.Test;

public class HomingMineTest
{
    [Fact]
    public void TestDefaultValues()
    {
        HomingMine homingMine = new();
        Assert.Equal(1, homingMine.Speed);
        Assert.Equal(0, homingMine.Delay);
    }
}