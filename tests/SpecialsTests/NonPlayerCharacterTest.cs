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
        npc.Rotation = double.NaN;
    }

    [Fact]
    public void TestSettingPoseAnglesAsNaN()
    {
        NonPlayerCharacter npc = new();
        npc.NeckAngle = double.NaN;
        npc.FrontArmAngle = double.NaN;
        npc.BackArmAngle = double.NaN;
        npc.FrontElbowAngle = double.NaN;
        npc.BackElbowAngle = double.NaN;
        npc.FrontLegAngle = double.NaN;
        npc.BackLegAngle = double.NaN;
        npc.FrontKneeAngle = double.NaN;
        npc.BackKneeAngle = double.NaN;
    }
}