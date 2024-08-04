using Xunit;
using System;

namespace LevelXML.Test;

public class TrashCanTest
{
    [Fact]
    public void TestDefaultValues()
    {
        TrashCan trashCan = new();
        Assert.Equal(0, trashCan.Rotation);
        Assert.False(trashCan.Sleeping);
        Assert.True(trashCan.Interactive);
        Assert.True(trashCan.HasTrash);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        TrashCan trashCan = new();
        trashCan.Rotation = double.NaN;
    }
}