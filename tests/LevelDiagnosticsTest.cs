using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LevelXML.Test;

public class LevelDiagnosticsTest
{
    [Fact]
    public void NoExceptions()
    {
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(new Rectangle()).ToList();
        Assert.Empty(exceptions);
    }

    [Fact]
    public void TestJointInGroup()
    {
        PinJoint joint = new();
        Group group = new(joint);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("PinJoint are not allowed in groups.", exception.Message);
        Assert.Equal(joint, exception.FaultyTag);
    }

    [Fact]
    public void TestSoccerBallPointedToByTrigger()
    {
        SoccerBall ball = new();
        Target<Special> target = new(ball);
        ActivateTrigger trigger = new(target);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(trigger, ball).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("SoccerBall cannot be pointed to by triggers.", exception.Message);
        Assert.Equal(target, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNCharacterType()
    {
        Level level = new()
        {
            Character = Character.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(level).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Level cannot have a NaN character type.", exception.Message);
        Assert.Equal(level, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNBottleType()
    {
        Bottle bottle = new()
        {
            BottleType = BottleType.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(bottle).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Bottle cannot have a NaN type.", exception.Message);
        Assert.Equal(bottle, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNBottleTypeInGroup()
    {
        Bottle bottle = new()
        {
            BottleType = BottleType.None,
            Interactive = false,
        };
        Group group = new(bottle);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Bottle cannot have a NaN type.", exception.Message);
        Assert.Equal(bottle, exception.FaultyTag);
    }

    [Fact]
    public void TestNaNFoodType()
    {
        Food food = new()
        {
            FoodType = FoodType.None
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(food).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("Food cannot have a NaN type.", exception.Message);
        Assert.Equal(food, exception.FaultyTag);
    }

    [Fact]
    public void TestInteractiveNPCInGroup()
    {
        NonPlayerCharacter npc = new()
        {
            Interactive = true
        };
        Group group = new(npc);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (LevelWouldFreezeOnStartException)exceptions.First();
        Assert.Equal("NonPlayerCharacter cannot be interactive and in groups.", exception.Message);
        Assert.Equal(npc, exception.FaultyTag);
    }

    [Fact]
    public void TestInteractiveBoomboxInGroup()
    {
        Boombox boombox = new()
        {
            Interactive = true
        };
        Group group = new(boombox);
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(group).ToList();
        var exception = (SpecialInvisibleAndNotInGroupException)exceptions.First();
        Assert.Equal("Boombox cannot be interactive and in groups.", exception.Message);
        Assert.Equal(boombox, exception.FaultySpecial);
    }

    [Fact]
    public void TestJetWithNaNPower()
    {
        Jet jet = new()
        {
            Power = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(jet).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("Jet cannot have NaN power.", exception.Message);
        Assert.Equal(jet, exception.FaultyEntity);
    }

    [Fact]
    public void TestNaNLengthSpikes()
    {
        SpikeSet spikes = new()
        {
            Spikes = double.NaN,
            Fixed = false
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(spikes).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("Nonfixed spike sets cannot have NaN spikes.", exception.Message);
        Assert.Equal(spikes, exception.FaultyEntity);
    }

    [Fact]
    public void TestNaNSlidingJointAngle()
    {
        Rectangle rect = new()
        {
            Fixed = false
        };
        SlidingJoint joint = new(rect)
        {
            Angle = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(rect, joint).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        Assert.Equal("A NaN angle in a sliding joint would black hole the attached entities.", exception.Message);
        Assert.Equal(joint, exception.FaultyEntity);
    }

    [Fact]
    public void TestNonFixedNaNDensityShape()
    {
        Triangle triangle = new()
        {
            Fixed = false,
            Density = double.NaN
        };
        IList<LevelXMLException> exceptions = LevelDiagnostics.GetExceptions(triangle).ToList();
        var exception = (EntityWouldBeBlackHoleException)exceptions.First();
        // People can ignore this if they're making a black hole on purpose
        Assert.Equal("A NaN density in a non fixed shape makes it a black hole.", exception.Message);
        Assert.Equal(triangle, exception.FaultyEntity);
    }
}