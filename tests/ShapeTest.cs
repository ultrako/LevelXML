using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LevelXML.Test;

public class ShapeTest
{
    [Fact]
    public void ShapeMissingX()
    {
		_ = new Rectangle(@"<sh t=""0"" p1=""0"" p2=""0""/>");
    }
	[Fact]
	public void ShapeMissingY()
    {
		_ = new Rectangle(@"<sh t=""0"" p0=""0"" p2=""0""/>");
    }
	[Fact]
	public void ShapeNaNRotation()
    {
		_ = new Rectangle(@"<sh t=""0"" p0=""0"" p1=""0"" p4=""NaN""/>");
    }
	[Fact]
	public void ShapeTestDefaultValues()
	{
		Rectangle rect = new();
		Assert.True(rect.Interactive);
		Assert.Equal(0, rect.X);
		Assert.Equal(0, rect.Y);
		Assert.Equal(0, rect.Rotation);
		Assert.True(rect.Fixed);
		Assert.False(rect.Sleeping);
		Assert.Equal(1, rect.Density);
		Assert.Equal(4032711, rect.FillColor);
		Assert.Equal(-1, rect.OutlineColor);
		Assert.Equal(100, rect.Opacity);
		Assert.Equal(Collision.Everything, rect.Collision);
	}

	[Fact]
	public void ShapeTestMinimal()
	{
		// Testing against what I actually get when I import the tag below
		// into the happy wheels import box
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0""/>");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />",
			rect.ToXML(), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void ShapeTestNaN()
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
			rect.ToXML(), ignoreWhiteSpaceDifferences: true);
    }

	[Fact]
	public void ShapeTestLowValues()
	{
		// So negative values for rotation are weird
		// I don't wanna reverse engineer the logic for it,
		// because it's useless, so just this once test differs
		// from import box.
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""-1000"" p7=""-1000"" p10=""-1000"" p11=""-1000"" />");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""-180"" p5=""t"" p6=""f"" p7=""0.1"" p8=""4032711"" p9=""-1"" p10=""0"" p11=""1"" />",
			rect.ToXML(), ignoreWhiteSpaceDifferences: true);
		
	}
	[Fact]
	public void ShapeTestHighValues()
	{
		Rectangle rect = new(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""123456789"" p7=""100"" p10=""123456789"" p11=""123456789"" />");
		Assert.Equal(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""180"" p5=""t"" p6=""f"" p7=""100"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""7"" />",
			rect.ToXML(), ignoreWhiteSpaceDifferences: true);
	}

	[Fact]
	public void RectangleTestDefaultValues()
	{
		Rectangle rect = new();
		Assert.Equal(300, rect.Width);
		Assert.Equal(100, rect.Height);
	}

	[Fact]
	public void RectangleTestNaNWidth()
	{
		_ = new Rectangle(@"<sh t=""0"" p0=""0"" p1=""0"" p2=""NaN"" />");
	}

	[Fact]
	public void RectangleTestNaNHeight()
	{
		_ = new Rectangle(@"<sh t=""0"" p0=""0"" p1=""0"" p3=""NaN"" />");
	}

	[Fact]
	public void RectangleTestHighSize()
	{
		Rectangle rect = new();
		rect.Width = double.PositiveInfinity;
		Assert.Equal(5000, rect.Width);
		rect.Height = double.PositiveInfinity;
		Assert.Equal(5000, rect.Height);
	}

	[Fact]
	public void RectangleTestLowSize()
	{
		Rectangle rect = new();
		rect.Width = double.NegativeInfinity;
		Assert.Equal(5, rect.Width);
		rect.Height = double.NegativeInfinity;
		Assert.Equal(5, rect.Height);
	}

	[Fact]
	public void TriangleTestDefaultValues()
	{
		Triangle triangle = new();
		Assert.Equal(200, triangle.Width);
		Assert.Equal(200, triangle.Height);
		Assert.Equal(@"<sh t=""2"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />",
			triangle.ToXML(), ignoreWhiteSpaceDifferences:true);
	}

	[Fact]
	public void TriangleTestNaNWidth()
	{
		_ = new Triangle(@"<sh t=""2"" p0=""0"" p1=""0"" p2=""NaN"" />");
	}

	[Fact]
	public void TriangleTestNaNHeight()
	{
		_ = new Triangle(@"<sh t=""2"" p0=""0"" p1=""0"" p3=""NaN"" />");
	}

	[Fact]
	public void TriangleTestLowValues()
	{
		Triangle triangle = new();
		triangle.Width = double.NegativeInfinity;
		triangle.Height = double.NegativeInfinity;
		Assert.Equal(5, triangle.Width);
		Assert.Equal(15, triangle.Height);
	}

	[Fact]
	public void CircleTestDefaultValues()
	{
		Circle circle = new();
		Assert.Equal(200, circle.Width);
		Assert.Equal(200, circle.Height);
		Assert.Equal(0, circle.Cutout);
	}

	[Fact]
	public void CircleTestNaNWidth()
	{
		_ = new Circle(@"<sh t=""1"" p0=""0"" p1=""0"" p2=""NaN"" />");
	}

	[Fact]
	public void CircleTestNaNHeight()
	{
		_ = new Circle(@"<sh t=""1"" p0=""0"" p1=""0"" p3=""NaN"" />");
	}

	[Fact]
	public void CircleTestHighSize()
	{
		Circle circle = new();
		circle.Width = double.PositiveInfinity;
		circle.Cutout = double.PositiveInfinity;
		Assert.Equal(5000, circle.Width);
		Assert.Equal(5000, circle.Height);
		Assert.Equal(100, circle.Cutout);
	}

	[Fact]
	public void CircleTestLowSize()
	{
		Circle circle = new();
		circle.Width = double.NegativeInfinity;
		circle.Cutout = double.NegativeInfinity;
		Assert.Equal(5, circle.Width);
		Assert.Equal(5, circle.Height);
		Assert.Equal(0, circle.Cutout);
	}

	[Fact]
	public void ArtTest()
	{
		Art art = new(new Vertex(new(0, 0)), new Vertex(new(100, 0)));
		string expected = @"<sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""0"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
  <v f=""t"" id=""0"" v0=""0_0"" v1=""100_0"" n=""2"" />
</sh>";
		string actual = art.ToXML(mapper: entity => 0);
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
		Assert.Equal(100, art.Width);
		Assert.Equal(0, art.Height);
	}
	
	[Fact]
	public void ArtTestWithVerticesInConstructor()
	{
		Art art = new(Art.EditorDefault, new(new(0, 0)), new(new(0, 100)));
		string expected = @"<sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""0"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
  <v f=""t"" id=""0"" v0=""0_0"" v1=""0_100"" n=""2"" />
</sh>";
		string actual = art.ToXML(mapper: entity => 0);
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
		Assert.Equal(0, art.Width);
		Assert.Equal(100, art.Height);
	}

	public static TheoryData<IEnumerable<Point>, double, double> 
	ArtShapeDimensionDefaultingTestCases => new() 
	{
        { new Point[]{new(-1,-1), new(1,1)}, 2,2 },
		{ new Point[]{new(-2,-2), new(-4,-4)}, 4,4 },
		{ new Point[]{new(-2,-2)}, double.NaN,double.NaN },
		{ new Point[]{}, double.NaN,double.NaN },
    };


	[Theory]
	[MemberData(nameof(ArtShapeDimensionDefaultingTestCases))]
	public void ArtTestDimensionDefaulting(IList<Point> points, double expectedWidth, double expectedHeight)
	{
		string numberOfVertices = @" n=""" + points.Count + @"""";
		string vertices = string.Join(String.Empty, 
		points.Select((point, index) => " v" + index.ToString() + @"=""" + point.X + "_" + point.Y + @""" "));
		Art art = new(@"<sh t=""4"" i=""f"" p0=""0"" p1=""0"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
  <v f=""t"" id=""0""" + numberOfVertices + vertices +@" />
</sh>");
		Assert.Equal(expectedWidth, art.Width);
		Assert.Equal(expectedHeight, art.Height);
	}

	// Yes, the happy wheels import box also supports this way of formatting vertices.
	// No idea why, but I'm mad about it.
	[Fact]
	public void ArtTestOtherVertexFormat()
	{
		Art art = new(@"<sh t=""3"" i=""f"" p0=""0"" p1=""0"" p2=""0"" p3=""0"" p4=""3"" p5=""t"" p6=""f"" p7=""1"" p8=""100000"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""0"" n=""3"" v0=""3.34"" v1=""2.-17"" v2=""0.-31""/>
    </sh>");
		Assert.Equal(3, art.Vertices[0].Position.X);
		Assert.Equal(34, art.Vertices[0].Position.Y);
		Assert.Equal(2, art.Vertices[1].Position.X);
		Assert.Equal(-17, art.Vertices[1].Position.Y);
		Assert.Equal(0, art.Vertices[2].Position.X);
		Assert.Equal(-31, art.Vertices[2].Position.Y);
	}

	[Fact]
	public void PolygonTestDefault()
	{
		Polygon poly = new();
		Assert.Equal(@"<sh t=""3"" p0=""0"" p1=""0"" p2=""NaN"" p3=""NaN"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">" +
		"\n  " + @"<v f=""t"" id=""0"" />" + "\n" + "</sh>",
		poly.ToXML(mapper: (ent) => 0), ignoreWhiteSpaceDifferences:true);
	}

	[Fact]
	public void CircleTestDefault()
	{
		Circle circle = new();
		Assert.Equal(@"<sh t=""1"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0"" />",
		circle.ToXML(), ignoreWhiteSpaceDifferences:true);
	}
}
