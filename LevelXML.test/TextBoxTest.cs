using Xunit;
using System;

namespace HappyWheels.Test;

public class TextBoxTest
{
    [Fact]
    public void TestDefault()
    {
        TextBox textbox = new();
        Assert.Equal(0, textbox.Rotation);
        Assert.Equal(0, textbox.Color);
        Assert.Equal(2, textbox.Font);
        Assert.Equal(15, textbox.FontSize);
        Assert.Equal(1, textbox.Alignment);
        Assert.Equal(100, textbox.Opacity);
        Assert.Equal("HERE'S SOME TEXT", textbox.Content);
    }

    [Fact]
    public void TestNaNRotation()
    {
        TextBox textbox = new();
        Assert.Throws<LevelXMLException>(() => textbox.Rotation = double.NaN);
    }

    [Fact]
    public void TestInvalidXMLInConstructor()
    {
        Assert.Throws<LevelXMLException>(() => new TextBox(@"<sh t=""1"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>"));
    }
}