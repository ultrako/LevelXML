using Xunit;
using System;

namespace HappyWheels.Test;

public class NPCTypeTest
{
    [Fact]
    public void TestEqualOperator()
    {
        NPCType character = NPCType.WheelchairGuy;
        Assert.True(NPCType.WheelchairGuy == character);
    }

    [Fact]
    public void TestNotEqualOperator()
    {
        NPCType character = NPCType.SegwayGuy;
        Assert.True(NPCType.IrresponsibleDad != character);
    }

    [Fact]
    public void TestEquals()
    {
        NPCType character = NPCType.EffectiveShopper;
        Assert.True(character.Equals(NPCType.EffectiveShopper));
    }

    [Fact]
    public void TestNotEquals()
    {
        NPCType character = NPCType.WheelchairGuy;
        Assert.False(character.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        NPCType character = NPCType.WheelchairGuy;
        Assert.Equal((1.0).GetHashCode(), character.GetHashCode());
    }

    [Fact]
    public void NPCTypeAsNaN()
    {
        NPCType character;
        Assert.Throws<LevelXMLException>(() => character = double.NaN);
    }
}