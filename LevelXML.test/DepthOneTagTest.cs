using Xunit;
using System;
namespace HappyWheels.test;

public class DepthOneTagTest
{
    [Fact]
    public void TestShapesTagWithOneRectangle()
    {
		Rectangle rect = new();
		DepthOneTag<Shape> shapes = new(rect);
		Assert.Equal(@"<shapes>
  <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
</shapes>",
			shapes.ToString(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
    }
	[Fact]
	public void TestImportShapesTag()
	{
		DepthOneTag<Shape> shapes = new(@"<shapes>
  <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
</shapes>");
		Assert.Equal((new Rectangle()).ToString(), shapes[0].ToString());
		
	}
	[Fact]
	public void TestTriggersTagWithSelfTarget()
	{
		//Trigger trigger = new(targets: new Target<Trigger>(trigger, new Activate()));
		// oops, use of variable trigger before it's assigned
		Trigger trigger = new();
		trigger.Add(new Target<Trigger>(trigger, new Activate()));
		DepthOneTag<Trigger> triggers = new(trigger);
		Assert.Equal(@"<triggers>
	<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
	<t i=""" + trigger.GetHashCode() + @""">
		<a i=""0"" />
	</t>
	</t>
</triggers>",
			triggers.ToString(mapper: entity => entity.GetHashCode()),
			ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}
}
