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
	public void TestCreateLevelWithTriggerToRectangle()
	{
		Rectangle rect = new();
		Trigger trigger = new(targets: new Target<Shape>(rect, new AwakeFromSleep()));
		Level level = new(info: default!, rect, trigger);
		Assert.Equal(@"<levelXML>
	<info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
	<shapes>
		<sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
	</shapes>
	<triggers>
		<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
			<sh i=""0"">
				<a i=""0"" />
			</sh>
		</t>
	</triggers>
</levelXML>",
		level.ToString(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}
	[Fact]
	public void ParseLevelWithTriggerToRectangle()
	{
		Level level = new(@"<levelXML>
    <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    </shapes>
    <triggers>
        <t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""0"" />
            </sh>
        </t>
    </triggers>
</levelXML>");
		//Console.WriteLine(level.ToString());
		Assert.Equal(level.Shapes![0]!, level.Triggers![0]![0]!.Targeted);
	}
	[Fact]
	public void ParseLevelWithSelfReferencingTrigger()
	{
		Level level = new(@"<levelXML>
	<info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
	<triggers>
		<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
			<t i=""0"">
				<a i=""0"" />
			</t>
		</t>
	</triggers>
</levelXML>");
		Console.WriteLine(level.ToString());
		Assert.Equal(level.Triggers![0]!, level.Triggers![0]![0]!.Targeted);
	}
}
