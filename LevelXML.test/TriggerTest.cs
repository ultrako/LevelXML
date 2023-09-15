using Xunit;
using System;
namespace HappyWheels.Test;

public class TriggerTest
{
    [Fact]
    public void TestActivateTriggerDefaultValues()
    {
        ActivateTrigger trigger = new();
        Assert.Equal(0, trigger.X);
        Assert.Equal(0, trigger.Y);
        Assert.Equal(100, trigger.Width);
        Assert.Equal(100, trigger.Height);
        Assert.Equal(0, trigger.Rotation);
        // Change these two tests to use structs after I make them
        Assert.Equal(1, trigger.TriggeredBy);
        Assert.Equal(1, trigger.RepeatType);
        Assert.False(trigger.StartDisabled);
        Assert.Equal(0, trigger.Delay);
    }
}