using Xunit;
using System;

namespace LevelXML.Test;

public class DinnerTableTest
{
    [Fact]
    public void TestDefaultValues()
    {
        DinnerTable table = new();
        Assert.Equal(0, table.Rotation);
        Assert.False(table.Sleeping);
        Assert.True(table.Interactive);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        DinnerTable table = new();
        Assert.Throws<LevelXMLException>(() => table.Rotation = double.NaN);
    }
}