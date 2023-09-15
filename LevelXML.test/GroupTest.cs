using Xunit;
using System;
using System.Linq;

namespace HappyWheels.Test;
public class GroupTest
{
    private string removeWhiteSpace(string text)
    {
        return String.Concat(text.Where(c => !Char.IsWhiteSpace(c)));
    }
    [Fact]
    public void GroupWithShape()
    {
        Rectangle rect = new();
        rect.Fixed = false;
        Group group = new(rect);
        string expected =
        @"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
  <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
</g>";
        string actual = group.ToXML(mapper:default!);
        Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
    }

    [Fact]
    public void GroupWithTextbox()
    {
        TextBox text = new();
        text.Content = "hello";
        Group group = new(text);
        string expected =
        @"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">" + "\n  " +
        text.ToXML() +
"</g>";
        string actual = group.ToXML(mapper:default!);
        Assert.Equal(removeWhiteSpace(expected), removeWhiteSpace(actual), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void GroupWithJointThrows()
    {
        PinJoint joint = new();
        Group group = new(joint);
        Assert.Throws<LevelXMLException>(() => group.ToXML(mapper:default!));
    }

    [Fact]
    public void TestGroupDefaultValues()
    {
        Group group = new();
        Assert.Equal(0, group.X);
        Assert.Equal(0, group.Y);
        Assert.Equal(0, group.OriginX);
        Assert.Equal(0, group.OriginY);
        Assert.Equal(0, group.Rotation);
        Assert.False(group.Sleeping);
        Assert.False(group.Foreground);
        Assert.Equal(100, group.Opacity);
        Assert.False(group.Fixed);
        Assert.False(group.FixedRotation);
    }

    [Fact]
    public void TestHighValues()
    {
        Group group = new(@"<g x=""Infinity"" y=""Infinity"" r=""Infinity"" ox=""Infinity"" oy=""Infinity"" s=""f"" f=""f"" o=""9999999"" im=""f"" fr=""f"" />");
        Assert.Equal(double.PositiveInfinity, group.X);
        Assert.Equal(double.PositiveInfinity, group.Y);
        // Technically happy wheels messes with the rotation here but it stays high
        // And Infinity turns to NaN
        Assert.Equal(double.PositiveInfinity, group.Rotation);
        Assert.Equal(double.PositiveInfinity, group.OriginX);
        Assert.Equal(double.PositiveInfinity, group.OriginY);
        Assert.Equal(100, group.Opacity);
    }

    [Fact]
    public void TestLowValues()
    {
        Group group = new(@"<g x=""-Infinity"" y=""-Infinity"" r=""-Infinity"" ox=""-Infinity"" oy=""-Infinity"" s=""f"" f=""f"" o=""-9999999"" im=""f"" fr=""f"" />");
        Assert.Equal(double.NegativeInfinity, group.X);
        Assert.Equal(double.NegativeInfinity, group.Y);
        // Technically happy wheels messes with the rotation here but it stays high
        // And Infinity turns to NaN
        Assert.Equal(double.NegativeInfinity, group.Rotation);
        Assert.Equal(double.NegativeInfinity, group.OriginX);
        Assert.Equal(double.NegativeInfinity, group.OriginY);
        Assert.Equal(0, group.Opacity);
    }

    [Fact]
    public void TestNaNXThrows()
    {
        Assert.Throws<LevelXMLException>(() => new Group(@"<g x=""NaN"" y=""0"" r=""0"" ox=""0"" oy=""0"" />"));
    }

    [Fact]
    public void TestNaNYThrows()
    {
        Assert.Throws<LevelXMLException>(() => new Group(@"<g x=""0"" y=""NaN"" r=""0"" ox=""0"" oy=""0"" />"));
    }

    [Fact]
    public void TestNaNRotationThrows()
    {
        Assert.Throws<LevelXMLException>(() => new Group(@"<g x=""0"" y=""0"" r=""NaN"" ox=""0"" oy=""0"" />"));
    }

    [Fact]
    public void TestNaNOriginXThrows()
    {
        Assert.Throws<LevelXMLException>(() => new Group(@"<g x=""0"" y=""0"" r=""0"" ox=""NaN"" oy=""0"" />"));
    }

    [Fact]
    public void TestNaNOriginYThrows()
    {
        Assert.Throws<LevelXMLException>(() => new Group(@"<g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""NaN"" />"));
    }
}