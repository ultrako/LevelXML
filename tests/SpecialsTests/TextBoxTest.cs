using Xunit;
using System;

namespace LevelXML.Test;

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
        textbox.Rotation = double.NaN;
    }

    [Fact]
    public void TestBlankContent()
    {
        // Test to ensure that initializing a textbox with no content does not throw
        var _ = new TextBox(@"<sp t=""7"" p0=""0"" p1=""0"" p2=""0"" p3=""333333"" p4=""3"" p5=""5"" p6=""1"" p8=""100"" />");
    }
}