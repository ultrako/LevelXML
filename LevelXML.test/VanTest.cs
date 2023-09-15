using Xunit;
using System;

namespace HappyWheels.Test;

public class VanTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Van van = new();
        Assert.Equal(0, van.Rotation);
        Assert.False(van.Sleeping);
        Assert.True(van.Interactive);
    }
}