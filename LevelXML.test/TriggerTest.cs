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
        Assert.Null(trigger.Interval);
        Assert.False(trigger.StartDisabled);
        Assert.Equal(0, trigger.Delay);
    }

    [Fact]
    public void TestSetStartDisabled()
    {
        ActivateTrigger trigger = new();
        trigger.StartDisabled = true;
        Assert.True(trigger.StartDisabled);
    }

    [Fact]
    public void TestVictoryTriggerDefault()
    {
        VictoryTrigger trigger = new();
        Assert.Equal(@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""3"" r=""1"" sd=""f"" />",
            trigger.ToXML(), ignoreWhiteSpaceDifferences: true);
    }
}