using Xunit;
using System;

namespace HappyWheels.Test;

public class MeteorTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Meteor meteor = new();
        Assert.Equal(400, meteor.Width);
        Assert.Equal(400, meteor.Height);
        Assert.False(meteor.Fixed);
        Assert.False(meteor.Sleeping);
    }

    [Fact]
    public void TestSettingWidthAsNaN()
    {
        Meteor meteor = new();
        Assert.Throws<LevelXMLException>(() => meteor.Width = double.NaN);
    }

    [Fact]
    public void TestSettingHeightAsNaN()
    {
        Meteor meteor = new();
        Assert.Throws<LevelXMLException>(() => meteor.Height = double.NaN);
    }
}