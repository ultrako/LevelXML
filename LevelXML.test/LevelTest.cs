using Xunit;
using System;

namespace HappyWheels.Test;

public class LevelTest
{
	[Fact]
	public void TestDefaultLevel()
	{
		Level level = new();
		Assert.Equal(@"<levelXML>
    <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
</levelXML>",
		level.ToString(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}
	[Fact]
	public void TestLevelWithTriggerToRectangle()
	{
		Rectangle rect = new();
		Trigger trigger = new(targets: new Target<Shape>(rect, new AwakeFromSleep()));
		Level level = new(info: default!, rect, trigger);
		Console.WriteLine(level.ToString());
	}
}
