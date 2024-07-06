using Xunit;

namespace LevelXML.Test;

public class NonPlayerCharacterTest
{
    [Fact]
    public void TestDefaultValues()
    {
        NonPlayerCharacter npc = new();
        Assert.Equal(0, npc.Rotation);
        Assert.Equal(NPCType.WheelchairGuy, npc.NPCType);
        Assert.False(npc.Sleeping);
        Assert.False(npc.Reverse);
        Assert.False(npc.HoldPose);
        Assert.True(npc.Interactive);
        Assert.Equal(0, npc.NeckAngle);
        Assert.Equal(0, npc.FrontArmAngle);
        Assert.Equal(0, npc.BackArmAngle);
        Assert.Equal(0, npc.FrontElbowAngle);
        Assert.Equal(0, npc.BackElbowAngle);
        Assert.Equal(0, npc.FrontLegAngle);
        Assert.Equal(0, npc.BackLegAngle);
        Assert.Equal(0, npc.FrontKneeAngle);
        Assert.Equal(0, npc.BackKneeAngle);
        Assert.False(npc.ReleaseOnDeath);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        NonPlayerCharacter npc = new();
        Assert.Throws<LevelXMLException>(() => npc.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingPoseAnglesAsNaN()
    {
        NonPlayerCharacter npc = new();
        Assert.Throws<LevelXMLException>(() => npc.NeckAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.FrontArmAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.BackArmAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.FrontElbowAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.BackElbowAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.FrontLegAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.BackLegAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.FrontKneeAngle = double.NaN);
        Assert.Throws<LevelXMLException>(() => npc.BackKneeAngle = double.NaN);
    }
}