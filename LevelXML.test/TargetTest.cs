using Xunit;
using System;

namespace HappyWheels.Test;

public class TargetTest {
	[Fact]
	public void AwakeFromSleepTargetTest()
	{
		Rectangle rect = new();
		Target<Shape> target = new(rect, new AwakeFromSleep());
		Assert.Equal(@"<sh i=""" + rect.GetHashCode() + @""">
 <a i=""0"" />
</sh>",
			target.ToXML(mapper: entity => entity.GetHashCode()),
			ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}
}
