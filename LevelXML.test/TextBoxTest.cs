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
}