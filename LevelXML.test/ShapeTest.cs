using Xunit;
using System;
namespace HappyWheels.test;

public class ShapeTest
{
    [Fact]
    public void RectangleTestBlank()
    {
		Assert.Throws<Exception>(() => new Rectangle("<sh />"));
    }
	[Fact]
	public void RectangleTestMinimal()
	{
		// Testing against what I actually get when I import the tag below
		// into the happy wheels import box
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0""/>");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />",
			rect.ToString(), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void RectangleTestNaN()
	{
		// So I know I went through a lot of effort making an interface that lets you
		// do special default behaviors per every single XAttribute on a levelXML tag
		// But this comes down to the fact that just saying "it does what the import
		// box does" is simpler and easier to understand.
		// Plus, NaN values lead to unique glitches that sometimes people want.
		// Otherwise, it'd just be writing code very tightly to the tests,
		// "This is what I want it to do to NaN" in the test,
		// and "if this is NaN do that" in the implementation.
		// Basically, I'll allow any NaN as long as the shape
		// won't disappear because of it.
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0"" p5=""NaN"" p6=""NaN"" p7=""NaN"" p8=""NaN"" p9=""NaN"" p10=""NaN"" p11=""NaN"" />");
        Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""NaN"" p6=""NaN"" p7=""NaN"" p8=""NaN"" p9=""NaN"" p10=""NaN"" p11=""NaN"" />",
			rect.ToString(), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void RectangleTestLowValues()
	{
		// So negative values for rotation are weird
		// I don't wanna reverse engineer the logic for it,
		// because it's useless, so just this once test differs
		// from import box.
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""-1000"" p3=""-1000"" p4=""-1000"" p7=""-1000"" p10=""-1000"" p11=""-1000"" />");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""5"" p3=""5"" p4=""-180"" p5=""t"" p6=""f"" p7=""0.1"" p8=""4032711"" p9=""-1"" p10=""0"" p11=""1"" />",
			rect.ToString(), ignoreWhiteSpaceDifferences: true);
		
	}
	[Fact]
	public void RectangleTestHighValues()
	{
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""123456789"" p3=""123456789"" p4=""123456789"" p7=""123456789"" p10=""123456789"" p11=""123456789"" />");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""5000"" p3=""5000"" p4=""180"" p5=""t"" p6=""f"" p7=""100"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""7"" />",
			rect.ToString(), ignoreWhiteSpaceDifferences: true);
	}
}
