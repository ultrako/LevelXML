using Xunit;
using System;

namespace LevelXML.Test;

public class SoundTriggerTest
{
    [Fact]
    public void TestDefaultValues()
    {
        SoundTrigger trigger = new();
        Assert.Equal(0, trigger.Sound);
        Assert.False(trigger.IsLocal);
        Assert.Equal(0, trigger.Panning);
        Assert.Equal(1, trigger.Volume);
        Assert.Equal(@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""2"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""1"" p=""0"" v=""1"" />",
        trigger.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestSoundIDInvalid()
    {
        SoundTrigger trigger = new();
        trigger.Sound = 326;
    }

    [Fact]
    public void TestSoundLocationLocal()
    {
        SoundTrigger trigger = new();
        trigger.IsLocal = true;
        Assert.Equal(@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""2"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""2"" p=""0"" v=""1"" />",
        trigger.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
}