using Xunit;
using System;
namespace LevelXML.Test;

public class SpecialTest
{
    [Fact]
    public void TestSpecialNaNX()
    {
        _ = new TextBox(@"<sp t=""16"" p0=""NaN"" p1=""0"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"" />");
    }

    [Fact]
    public void TestSpecialNaNY()
    {
        _ = new TextBox(@"<sp t=""16"" p0=""0"" p1=""NaN"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"" />");
    }

    [Fact]
    public void TestProperties()
    {
        TextBox special = new();
        special.X = 500;
        special.Y = 300;
        Assert.Equal(500, special.X);
        Assert.Equal(300, special.Y);
    }
}