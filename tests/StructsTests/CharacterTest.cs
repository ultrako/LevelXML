using Xunit;
using System;

namespace LevelXML.Test;

public class CharacterTest
{
    [Fact]
    public void TestEqualOperator()
    {
        Character character = Character.WheelchairGuy;
        Assert.True(Character.WheelchairGuy == character);
    }

    [Fact]
    public void TestNotEqualOperator()
    {
        Character character = Character.SegwayGuy;
        Assert.True(Character.IrresponsibleDad != character);
    }

    [Fact]
    public void TestEquals()
    {
        Character character = Character.EffectiveShopper;
        Assert.True(character.Equals(Character.EffectiveShopper));
    }

    [Fact]
    public void TestNotEquals()
    {
        Character character = Character.WheelchairGuy;
        Assert.False(character.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Character character = Character.WheelchairGuy;
        Assert.Equal((1.0).GetHashCode(), character.GetHashCode());
    }
}