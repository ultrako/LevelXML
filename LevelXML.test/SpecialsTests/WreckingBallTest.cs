using Xunit;
using System;

namespace HappyWheels.Test;

public class WreckingBallTest
{
    [Fact]
    public void TestDefaultValues()
    {
        WreckingBall wreckingBall = new();
        Assert.Equal(350, wreckingBall.RopeLength);
    }
}