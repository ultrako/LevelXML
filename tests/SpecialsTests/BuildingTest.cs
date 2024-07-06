using Xunit;
using System;

namespace LevelXML.Test;

public class BuildingTest
{
    [Fact]
    public void TestDefaultValues()
    {
        BuildingOne building = new();
        Assert.Equal(1, building.FloorWidth);
        Assert.Equal(3, building.Floors);
    }

    [Fact]
    public void TestSettingFloorWidthAsNaN()
    {
        BuildingTwo building = new();
        Assert.Throws<LevelXMLException>(() => building.FloorWidth = double.NaN);
    }
}