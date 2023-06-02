using Xunit;
using System;

namespace HappyWheels.test;

public class TextBoxTest
{
    [Fact]
    public void TestDefault()
    {
        TextBox textbox = new();
        string expected = @"<sp t=""16"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
    <p7><![CDATA[HERE'S SOME TEXT]]></p7>
</sp>";
        string actual = textbox.ToXML();
        Assert.Equal(expected,
        actual,
        ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
    }
}