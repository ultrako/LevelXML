using Xunit;
using System;

namespace HappyWheels.test;
public class GroupTest
{
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
        string actual = group.ToString();
        Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
    }
}