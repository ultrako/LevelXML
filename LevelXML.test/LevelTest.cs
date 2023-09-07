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
		level.ToXML(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}
	[Fact]
	public void TestCreateLevelWithTriggerToRectangle()
	{
		Rectangle rect = new();
		Trigger trigger = new(targets: new Target<Shape>(rect, new AwakeFromSleep()));
		Level level = new(rect, trigger);
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
		level.ToXML(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
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
		Assert.Equal(level.Triggers![0]!, level.Triggers![0]![0]!.Targeted);
	}
	[Fact]
	public void TestLevelWithGroupThatHasArtShape()
	{
		Art art = new();
		art.Vertices.Add(new(new(3,0)));
		Group group = new(art);
		Level level = new(group);
		string expected = @"<levelXML>
  <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <groups>
    <g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
      <sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
        <v f=""t"" id=""0"" n=""1"" v0=""3_0"" />
      </sh>
    </g>
  </groups>
</levelXML>";
		string actual = level.ToXML();
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
	}
	[Fact]
	public void ArtShapeTestNoIDCollision()
	{
		Art art1 = new();
		art1.Vertices.Add(new(new(3,0)));
		Art art2 = new();
		Level level = new(art1, art2);
		string expected =@"<levelXML>
  <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <shapes>
    <sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""0"" n=""1"" v0=""3_0"" />
    </sh>
    <sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""1"" />
    </sh>
  </shapes>
</levelXML>";
		string actual = level.ToXML();
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
	}
	[Fact]
	public void ParseLevelWithArt()
	{
		String levelXML = @"<levelXML>
    <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""4"" i=""f"" p0=""466"" p1=""5200"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" n=""3"" v0=""28_47"" v1=""-28_-53"" v2=""-27_53"" />
        </sh>
        <sh t=""4"" i=""f"" p0=""537"" p1=""5151"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" />
        </sh>
    </shapes>
</levelXML>";
		Level level = new(levelXML);
		// I'd like to assert that the vertex id for these two art shapes ends up the same -
		// but vertex ids are not exposed as public, so I'll just see if the library spits back the same level.
		Assert.Equal(levelXML, level.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}
	[Fact]
	public void ParseLevelWithJoint()
	{
		Level level = new(@"<levelXML>
    <info v=""1.94"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""0"" p0=""526"" p1=""5235"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
        <sh t=""0"" p0=""587"" p1=""5298"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    </shapes>
    <joints>
        <j t=""0"" x=""603"" y=""5263"" b1=""1"" b2=""0"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
    </joints>
</levelXML>");
		Assert.Equal(level.Shapes[1], level.Joints[0].First);
		Assert.Equal(level.Shapes[0], level.Joints[0].Second);
	}
}
