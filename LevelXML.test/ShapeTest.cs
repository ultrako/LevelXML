using Xunit;
using System;
namespace HappyWheels.test;

public class ShapeTest
{
    [Fact]
    public void RectangleTest()
    {
		Rectangle rect = new();
		Assert.Equal(300, rect.Width);
		Console.WriteLine(rect.ToXElement());
    }
}
