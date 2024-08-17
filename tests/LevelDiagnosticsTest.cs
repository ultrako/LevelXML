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
        Assert.Equal("SoccerBall cannot be pointed to by triggers!", exception.Message);
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
        Assert.Equal("Level cannot have a NaN character type!", exception.Message);
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
        Assert.Equal("Bottle cannot have a NaN type!", exception.Message);
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
        Assert.Equal("Bottle cannot have a NaN type!", exception.Message);
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
        Assert.Equal("Food cannot have a NaN type!", exception.Message);
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
}