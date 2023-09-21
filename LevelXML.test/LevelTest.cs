using Xunit;
using System;
using System.Linq;

namespace HappyWheels.Test;

// So this entity type is to be able to help more thoroughly test the cases on switches on Entities
public class TestEntity : Entity
{
	public override double? X { get { return null; } set {}}
	public override double? Y { get { return null; } set {}}
	public TestEntity() : base("z") {}
}

// And this target has a type param of that entity

public class TestTarget : Target<TestEntity>
{
	public TestTarget() : base(new TestEntity()) {}
}

public class LevelTest
{
	[Fact]
	public void TestDefaultLevel()
	{
		Level level = new();
		Assert.Equal(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
		Assert.Equal(300, level.X);
		Assert.Equal(5100, level.Y);
		Assert.Equal(Character.WheelchairGuy, level.Character);
		Assert.False(level.ForcedCharacter);
		Assert.False(level.VehicleHidden);
		Assert.Equal(0, level.Background);
		Assert.Equal(16777215, level.BackgroundColor);
	}

	[Fact]
	public void TestMergeLevels()
	{
		Rectangle rectangle = new();
		Circle circle = new();
		PinJoint joint = new(rectangle, circle);
		Level level1 = new(circle);
		Level level2 = new(rectangle);
		Level level3 = new(level1.Entities.Concat(level2.Entities).Concat(new[] {joint}).ToArray());
		Assert.Equal(joint.First, level3.Shapes[1]);
		Assert.Equal(joint.Second, level3.Shapes[0]);
	}

	[Fact]
	public void TestLevelWithNaNCharacter()
	{
		Level level = new();
		Assert.Throws<LevelXMLException>(() => level.Character = double.NaN);
	}

	[Fact]
	public void TestParseLevelWithNoInfoTag()
	{
		Assert.Throws<LevelXMLException>(() => new Level("<levelXML />"));
	}

	[Fact]
	public void TestParseLevelWithWrongLevelXMLTag()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<level>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
</level>"));
	}

	[Fact]
	public void TestLevelWithDisableMotorTrigger()
	{
		PinJoint joint = new();
		ActivateTrigger trigger = new();
		trigger.AddTarget(new Target<Joint>(joint, new DisableMotor()));
		Level level = new(trigger, joint);
		Assert.Equal(@"<levelXML>
  <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <joints>
    <j t=""0"" x=""0"" y=""0"" b1=""-1"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
  </joints>
  <triggers>
    <t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
      <j i=""0"">
        <a i=""0"" />
      </j>
    </t>
  </triggers>
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences: true);
	}

	[Fact]
	public void ParseLevelWithInvalidE()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""2""/>
</levelXML>"));
	}

	[Fact]
	public void TestCreateLevelWithTriggerToRectangle()
	{
		Rectangle rect = new();
		ActivateTrigger trigger = new(targets: new Target<Shape>(rect, new AwakeShapeFromSleep()));
		Level level = new(rect, trigger);
		Assert.Equal(@"<levelXML>
	<info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
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
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
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
		Assert.Equal(level.Shapes![0]!, ((ActivateTrigger)level.Triggers![0]!).Targets[0]!.Targeted);
	}

	[Fact]
	public void ParseLevelWithTriggerAwakingVan()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""0"" p0=""292"" p1=""5254"" p2=""0"" p3=""f"" p4=""t""/>
    </specials>
    <triggers>
        <t x=""409"" y=""5113"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<SimpleSpecial> target = (Target<SimpleSpecial>)trigger.Targets[0];
		Assert.IsType<AwakeSpecialFromSleep>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerToSoccerBall()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""10"" p0=""457"" p1=""5199""/>
    </specials>
    <triggers>
        <t x=""403"" y=""5357"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0""/>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithGroupedSoccerBall()
	{
		// I add a ToXML() at the end here as group validity checking should only happen then
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""514"" y=""5321"" r=""0"" ox=""-514"" oy=""-5321"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sp t=""10"" p0=""385"" p1=""5317""/>
        </g>
    </groups>
</levelXML>").ToXML());
	}

	[Fact]
	public void ParseLevelWithAwakeGroup()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""619"" y=""5240"" r=""0"" ox=""-619"" oy=""-5240"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""1"" p0=""619"" p1=""5240"" p2=""200"" p3=""200"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>     
        </g>
    </groups>
    <triggers>
        <t x=""313"" y=""5176"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""0""/>
            </g>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<Group> target = (Target<Group>)trigger.Targets[0];
		Assert.IsType<AwakeGroupFromSleep>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithDisableMotor()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""1"" p0=""586"" p1=""5251"" p2=""200"" p3=""200"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>
    </shapes>
    <joints>
        <j t=""0"" x=""483"" y=""5225"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""313"" y=""5176"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""0""/>
            </j>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<Joint> target = (Target<Joint>)trigger.Targets[0];
		Assert.IsType<DisableMotor>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidTriggerTarget()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""313"" y=""5176"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <john i=""0"">
                <a i=""0""/>
            </john>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithSelfReferencingTrigger()
	{
		Level level = new(@"<levelXML>
	<info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
	<triggers>
		<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
			<t i=""0"">
				<a i=""0"" />
			</t>
		</t>
	</triggers>
</levelXML>");
		Assert.Equal(level.Triggers![0]!, ((ActivateTrigger)level.Triggers![0]!).Targets[0]!.Targeted);
	}

	[Fact]
	public void LevelWithSelfReferencingTrigger()
	{
		ActivateTrigger trigger = new();
		trigger.AddTarget(new Target<Trigger>(trigger, new Activate()));
		Level level = new(trigger);
		Assert.Equal(@"<levelXML>
  <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <triggers>
    <t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
      <t i=""0"">
        <a i=""0"" />
      </t>
    </t>
  </triggers>
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences:true);
	}

	[Fact]
	public void LevelWithTriggerReferencingEntityNotInLevel()
	{
		Rectangle rect = new();
		ActivateTrigger trigger = new();
		trigger.AddTarget(new Target<Shape>(rect, new AwakeShapeFromSleep()));
		Level level = new(trigger);
		Assert.Throws<LevelXMLException>(() => level.ToXML());
	}

	[Fact]
	public void TestLevelWithGroupThatHasArtShape()
	{
		Art art = new();
		art.Vertices.Add(new(new(3,0)));
		Group group = new(art);
		Level level = new(group);
		string expected = @"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
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
		art2.Vertices.Add(new(new(3,3)));
		Level level = new(art1, art2);
		string expected =@"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <shapes>
    <sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""0"" n=""1"" v0=""3_0"" />
    </sh>
    <sh t=""4"" i=""f"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""1"" n=""1"" v0=""3_3"" />
    </sh>
  </shapes>
</levelXML>";
		string actual = level.ToXML();
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
	}

	[Fact]
	public void PolyShapeTestNoIDCollision()
	{
		Polygon poly1 = new();
		poly1.Vertices.Add(new(new(3,0)));
		Polygon poly2 = new();
		Level level = new(poly1, poly2);
		string expected =@"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <shapes>
    <sh t=""3"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""0"" n=""1"" v0=""3_0"" />
    </sh>
    <sh t=""3"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
      <v f=""t"" id=""1"" />
    </sh>
  </shapes>
</levelXML>";
		string actual = level.ToXML();
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
	}

	[Fact]
	public void TestLevelWithGroupThatHasPolyShape()
	{
		Polygon poly = new();
		poly.Vertices.Add(new(new(3,0)));
		Group group = new(poly);
		Level level = new(group);
		string expected = @"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <groups>
    <g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
      <sh t=""3"" p0=""0"" p1=""0"" p2=""100"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
        <v f=""t"" id=""0"" n=""1"" v0=""3_0"" />
      </sh>
    </g>
  </groups>
</levelXML>";
		string actual = level.ToXML();
		Assert.Equal(expected, actual, ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences:true);
	}

	[Fact]
	public void ParseLevelWithInvalidEntity()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <joints>
        <john t=""1"" x=""603"" y=""5263"" b1=""1"" b2=""0"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
    </joints>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithArt()
	{
		string levelXML = @"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""4"" i=""f"" p0=""466"" p1=""5200"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" n=""3"" v0=""28_47_99_88"" v1=""-28_-53"" v2=""-27_53_33_11_-14_30"" />
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
	public void TestArtShapeWithNoVertexId()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""4"" i=""f"" p0=""466"" p1=""5200"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" n=""3"" v0=""28_47"" v1=""-28_-53"" v2=""-27_53"" />
        </sh>
        <sh t=""4"" i=""f"" p0=""537"" p1=""5151"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" />
        </sh>
    </shapes>
</levelXML>"));
	}

	[Fact]
	public void TestArtShapeWithInvalidVertex()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""4"" i=""f"" p0=""537"" p1=""5151"" p2=""56"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" n=""1"" v0=""3""/>
        </sh>
    </shapes>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithPolys()
	{
		String levelXML = @"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""3"" p0=""466"" p1=""5200"" p2=""56"" p3=""106"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" n=""3"" v0=""28_47"" v1=""-28_-53"" v2=""-27_53"" />
        </sh>
        <sh t=""3"" p0=""537"" p1=""5151"" p2=""56"" p3=""106"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""0"" />
        </sh>
    </shapes>
</levelXML>";
		Level level = new(levelXML);
		Assert.Equal(levelXML, level.ToXML(), ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void ParseLevelWithOneBlankArtShapeWithInvalidID()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""309"" y=""5168"" c=""1"" f=""t"" h=""t"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""4"" i=""f"" p0=""358.50"" p1=""5339"" p2=""83"" p3=""106"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""1"" />
        </sh>
    </shapes>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithPinJoint()
	{
		Level level = new(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
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

	[Fact]
	public void ParseLevelWithSlidingJoint()
	{
		Level level = new(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <shapes>
        <sh t=""0"" p0=""526"" p1=""5235"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
        <sh t=""0"" p0=""587"" p1=""5298"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    </shapes>
    <joints>
        <j t=""1"" x=""6.3"" y=""26"" b1=""0"" b2=""1"" a=""90"" l=""f"" ul=""100"" ll=""-100"" m=""f"" fo=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>");
		Assert.Equal(level.Shapes[0], level.Joints[0].First);
		Assert.Equal(level.Shapes[1], level.Joints[0].Second);
	}

	[Fact]
	public void ParseLevelWithInvalidJointType()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <joints>
        <j t=""2"" x=""603"" y=""5263"" b1=""1"" b2=""0"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
    </joints>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithCircle()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""1"" p0=""394"" p1=""5358"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>
    </shapes>
</levelXML>");
		Assert.IsType<Circle>(level.Shapes[0]);
	}

	[Fact]
	public void ParseLevelWithTriangle()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""2"" p0=""0"" p1=""0"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
</levelXML>");
		Assert.IsType<Triangle>(level.Shapes[0]);
	}


	[Fact]
	public void ParseLevelWithPolygon()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""3"" p0=""425.50"" p1=""5321"" p2=""207"" p3=""158"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"">
            <v f=""t"" id=""1"" n=""3"" v0=""67.5_-79"" v1=""103.5_79"" v2=""-103.5_51""/>
        </sh>
    </shapes>
</levelXML>");
		Assert.IsType<Polygon>(level.Shapes[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidShapeType()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""5"" p0=""425.50"" p1=""5321"" p2=""207"" p3=""158"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    </shapes>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithSoundTrigger()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""596"" y=""5264"" w=""100"" h=""100"" a=""0"" b=""1"" t=""2"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""1"" p=""0"" v=""1""/>
    </triggers>
</levelXML>");
		Assert.IsType<SoundTrigger>(level.Triggers[0]);
	}

	[Fact]
	public void ParseLevelWithVictoryTrigger()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""3"" r=""1"" sd=""f""/>
    </triggers>
</levelXML>");
		Assert.IsType<VictoryTrigger>(level.Triggers[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidTriggerAction()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""596"" y=""5264"" w=""100"" h=""100"" a=""0"" b=""1"" t=""4"" r=""1"" sd=""f"" s=""0"" d=""0"" l=""1"" p=""0"" v=""1""/>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithTriggerFiringHarpoon()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""15"" p0=""121"" p1=""150.0517578125"" p2=""0"" p3=""t"" p4=""t"" p5=""0"" p6=""f"" p7=""f""/>
    </specials>
    <triggers>
        <t x=""106"" y=""299"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<Harpoon> target = (Target<Harpoon>)trigger.Targets[0];
		Assert.IsType<FireHarpoon>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerDeactivatingHarpoon()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""15"" p0=""121"" p1=""150.0517578125"" p2=""0"" p3=""t"" p4=""t"" p5=""0"" p6=""f"" p7=""f""/>
    </specials>
    <triggers>
        <t x=""106"" y=""299"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""1""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<Harpoon> target = (Target<Harpoon>)trigger.Targets[0];
		Assert.IsType<DeactivateHarpoon>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerActivatingHarpoon()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""15"" p0=""121"" p1=""150.0517578125"" p2=""0"" p3=""t"" p4=""t"" p5=""0"" p6=""f"" p7=""f""/>
    </specials>
    <triggers>
        <t x=""106"" y=""299"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""2""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Target<Harpoon> target = (Target<Harpoon>)trigger.Targets[0];
		Assert.IsType<ActivateHarpoon>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidHarpoonActionID()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""15"" p0=""121"" p1=""150.0517578125"" p2=""0"" p3=""t"" p4=""t"" p5=""0"" p6=""f"" p7=""f""/>
    </specials>
    <triggers>
        <t x=""106"" y=""299"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""3""/>
            </sp>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithChangeTextBoxOpacity()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""176"" y=""5138"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""16"" p0=""248"" p1=""5152"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[HERE'S SOME TEXT]]></p7>
        </sp>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""0"" p0=""100"" p1=""1""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<TextBox>)trigger.Targets[0];
		Assert.IsType<ChangeTextBoxOpacity>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithSlideTextBox()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""176"" y=""5138"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""16"" p0=""248"" p1=""5152"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[HERE'S SOME TEXT]]></p7>
        </sp>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""1"" p0=""10"" p1=""0"" p2=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<TextBox>)trigger.Targets[0];
		Assert.IsType<SlideTextBox>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidTextBoxActionID()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""176"" y=""5138"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""16"" p0=""248"" p1=""5152"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[HERE'S SOME TEXT]]></p7>
        </sp>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""2"" p0=""10"" p1=""0"" p2=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithAwakeNPC()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""105"" p1=""117"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
    <triggers>
        <t x=""142"" y=""297"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<NonPlayerCharacter>)trigger.Targets[0];
		Assert.IsType<AwakeNPCFromSleep>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithImpulseNPC()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""105"" p1=""117"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
    <triggers>
        <t x=""142"" y=""297"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""1"" p0=""10"" p1=""-10"" p2=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<NonPlayerCharacter>)trigger.Targets[0];
		Assert.IsType<ImpulseNPC>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithHoldPose()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""105"" p1=""117"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
    <triggers>
        <t x=""142"" y=""297"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""2""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<NonPlayerCharacter>)trigger.Targets[0];
		Assert.IsType<HoldPose>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithReleasePose()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""105"" p1=""117"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
    <triggers>
        <t x=""142"" y=""297"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""3""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		var target = (Target<NonPlayerCharacter>)trigger.Targets[0];
		Assert.IsType<ReleasePose>(target.Actions[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidNPCActionID()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""105"" p1=""117"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
    <triggers>
        <t x=""142"" y=""297"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""4""/>
            </sp>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithVan()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""0"" p0=""639"" p1=""5251"" p2=""0"" p3=""f"" p4=""t""/>
    </specials>
</levelXML>");
		Assert.IsType<Van>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithDinnerTable()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""108"" y=""124"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""1"" p0=""373"" p1=""114"" p2=""400"" p3=""32"" p4=""0"" p5=""f"" p6=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<DinnerTable>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerToLandmine()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""2"" p0=""243"" p1=""132"" p2=""0""/>
    </specials>
    <triggers>
        <t x=""198"" y=""290"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0""/>
        </t>
    </triggers>
</levelXML>");
		Assert.IsType<Landmine>(level.Specials[0]);
		ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
		Assert.IsType<Target<Landmine>>(trigger.Targets[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerToWreckingBall()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""7"" p0=""424.78761291503906"" p1=""5180"" p2=""350""/>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0""/>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		Assert.IsType<Target<WreckingBall>>(trigger.Targets[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerToFan()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""8"" p0=""479"" p1=""5355"" p2=""0""/>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0""/>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		Assert.IsType<Target<Fan>>(trigger.Targets[0]);
	}

	[Fact]
	public void ParseLevelWithTriggerToBoost()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""12"" p0=""523"" p1=""5138"" p2=""0"" p3=""2"" p4=""20""/>
    </specials>
    <triggers>
        <t x=""223"" y=""5279"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0""/>
        </t>
    </triggers>
</levelXML>");
		var trigger = (ActivateTrigger)level.Triggers[0];
		Assert.IsType<Target<Boost>>(trigger.Targets[0]);
	}

	[Fact]
	public void ParseLevelWithIBeam()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""3"" p0=""0"" p1=""0"" p4=""0"" p5=""f"" p6=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<IBeam>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithLog()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""4"" p0=""0"" p1=""0"" p4=""0"" p5=""f"" p6=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<Log>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithSpringPlatform()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""5"" p0=""0"" p1=""0"" p2=""0"" />
    </specials>
</levelXML>");
		Assert.IsType<SpringPlatform>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithSpikeSet()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""6"" p0=""0"" p1=""0"" p2=""0"" p3=""t"" p5=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<SpikeSet>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithWreckingBall()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""7"" p0=""0"" p1=""0"" p2=""350""/>
    </specials>
</levelXML>");
		Assert.IsType<WreckingBall>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithFan()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""8"" p0=""0"" p1=""0"" p2=""0""/>
    </specials>
</levelXML>");
		Assert.IsType<Fan>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithFinishLine()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""9"" p0=""0"" p1=""0"" />
    </specials>
</levelXML>");
		Assert.IsType<FinishLine>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithSoccerBall()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""10"" p0=""457"" p1=""5199""/>
    </specials>
</levelXML>");
		Assert.IsType<SoccerBall>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithMeteor()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""211"" y=""185"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""11"" p0=""0"" p1=""0"" p2=""400"" p4=""f"" p5=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<Meteor>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithBoost()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""12"" p0=""0"" p1=""0"" p2=""2"" p3=""20"" />
    </specials>
</levelXML>");
		Assert.IsType<Boost>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithBuildingOne()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""13"" p0=""0"" p1=""0"" p2=""1"" p3=""3""/>
    </specials>
</levelXML>");
		Assert.IsType<BuildingOne>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithBuildingTwo()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""14"" p0=""0"" p1=""0"" p2=""1"" p3=""3""/>
    </specials>
</levelXML>");
		Assert.IsType<BuildingTwo>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithHarpoon()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""111"" y=""39"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""15"" p0=""0"" p1=""0"" p2=""0"" p3=""t"" p4=""f"" p5=""0"" p6=""f"" p7=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<Harpoon>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithText()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""16"" p0=""553"" p1=""5320"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[hello]]></p7>
        </sp>
    </specials>
</levelXML>");
		Assert.Equal("hello", ((TextBox)level.Specials[0]).Content);
	}

	[Fact]
	public void ParseLevelWithNPC()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""214"" y=""5159"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""17"" p0=""106"" p1=""118"" p2=""0"" p3=""1"" p4=""f"" p5=""f"" p6=""f"" p7=""t"" p8=""0"" p9=""0"" p10=""0"" p11=""0"" p12=""0"" p13=""0"" p14=""0"" p15=""0"" p16=""0"" p17=""f""/>
    </specials>
</levelXML>");
		Assert.IsType<NonPlayerCharacter>(level.Specials[0]);
	}

	[Fact]
	public void ParseLevelWithInvalidSpecialType()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <specials>
        <sp t=""1234"" p0=""553"" p1=""5320"" p2=""0"" p3=""0"" p4=""2"" p5=""15"" p6=""1"" p8=""100"">
            <p7><![CDATA[hello]]></p7>
        </sp>
    </specials>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithJointToVan()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""0"" p0=""586"" p1=""5249"" p2=""0"" p3=""f"" p4=""t""/>
    </specials>
    <joints>
        <j t=""0"" x=""546"" y=""5223"" b1=""-1"" b2=""s0"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>");
		Assert.Equal(level.Specials[0], level.Joints[0].Second);
	}

	[Fact]
	public void ParseLevelWithJointToSoccerBall()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""10"" p0=""586"" p1=""5249"" />
    </specials>
    <joints>
        <j t=""0"" x=""546"" y=""5223"" b1=""-1"" b2=""s0"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>").ToXML());
	}

	[Fact]
	public void CreateLevelWithJointToVan()
	{
		Van van = new();
		PinJoint joint = new(van);
		Level level = new(van, joint);
		Assert.Equal(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
    <specials>
        <sp t=""0"" p0=""0"" p1=""0"" p2=""0"" p3=""f"" p4=""t"" />
    </specials>
    <joints>
        <j t=""0"" x=""0"" y=""0"" b1=""s0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
    </joints>
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences: true);
	}

	[Fact]
	public void CreateLevelWithPinJointToShapes()
	{
		Rectangle rect1 = new();
		rect1.Fixed = false;
		Rectangle rect2 = new();
		rect2.Fixed = false;
		PinJoint joint = new(rect1, rect2);
		Level level = new(new Entity[] {rect1, rect2, joint});
		Assert.Equal(@"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <shapes>
    <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
  </shapes>
  <joints>
    <j t=""0"" x=""0"" y=""0"" b1=""0"" b2=""1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
  </joints>
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences: true);
	}

	[Fact]
	public void CreateLevelWithSlidingJointToShapes()
	{
		Rectangle rect1 = new();
		rect1.Fixed = false;
		Rectangle rect2 = new();
		rect2.Fixed = false;
		SlidingJoint joint = new(rect1, rect2);
		Level level = new(rect1, rect2, joint);
		Assert.Equal(@"<levelXML>
  <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <shapes>
    <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
    <sh t=""0"" p0=""0"" p1=""0"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" />
  </shapes>
  <joints>
    <j t=""1"" x=""0"" y=""0"" b1=""0"" b2=""1"" l=""f"" ul=""100"" ll=""-100"" m=""f"" fo=""50"" a=""90"" sp=""3"" c=""f"" />
  </joints>
</levelXML>",
			level.ToXML(), ignoreWhiteSpaceDifferences:true);
	}

	[Fact]
	public void CreateLevelWithJointToGroup()
	{
		Group group = new();
		group.Fixed = false;
		PinJoint joint = new(group);
		Level level = new(new Entity[] {group, joint});
		Assert.Equal(@"<levelXML>
  <info v=""" + Info.HappyWheelsVersion + @""" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <groups>
    <g x=""0"" y=""0"" r=""0"" ox=""0"" oy=""0"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"" />
  </groups>
  <joints>
    <j t=""0"" x=""0"" y=""0"" b1=""g0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />
  </joints>
</levelXML>",
		level.ToXML(), ignoreWhiteSpaceDifferences:true);
	}

	[Fact]
	public void ParseLevelWithJointToGroup()
	{
		Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""364"" y=""5334"" r=""0"" ox=""-364"" oy=""-5334"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""1"" p0=""364"" p1=""5334"" p2=""200"" p3=""200"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>     
        </g>
    </groups>
    <joints>
        <j t=""0"" x=""387"" y=""5290"" b1=""g0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>");
		Assert.Equal(level.Groups[0], level.Joints[0].First);
	}

	[Fact]
	public void ParseLevelWithJointWithInvalidPrefixToIndex()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""309"" y=""5168"" c=""1"" f=""t"" h=""t"" bg=""0"" bgc=""16777215"" e=""1""/>
    <joints>
        <j t=""0"" x=""555"" y=""5372"" b1=""t0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>"));
	}

	[Fact]
	public void ParseLevelWithJointWithInvalidTargetID()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""309"" y=""5168"" c=""1"" f=""t"" h=""t"" bg=""0"" bgc=""16777215"" e=""1""/>
    <joints>
        <j t=""0"" x=""555"" y=""5372"" b1=""abcdef"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
</levelXML>"));
	}

	[Fact]
	public void JointToInvalidEntity()
	{
		PinJoint joint = new();
		Assert.Throws<LevelXMLException>(() => joint.First = joint);
	}

	[Fact]
	public void TestParseLevelWithInvalidTargetType()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""309"" y=""5168"" c=""1"" f=""f"" h=""t"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""1"" p0=""444"" p1=""5357"" p2=""200"" p3=""200"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1"" p12=""0""/>
    </shapes>
    <triggers>
        <t x=""280"" y=""5388"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <john i=""0""/>
        </t>
    </triggers>
</levelXML>"));
	}

	[Fact]
	public void TestParseLevelWithTargetTypeOfEmptyTag()
	{
		// Have to make this test this way because throwing in an async method wraps my exception
		try
		{
			new Level(@"<levelXML>
  <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1"" />
  <triggers>
    <t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
      <g i=""0"">
        <a i=""0"" />
      </g>
    </t>
  </triggers>
</levelXML>").ToXML();
		} catch (AggregateException e)
		{
			Assert.IsType<LevelXMLException>(e.InnerException);
		}
	}

	[Fact]
	public void TestLevelWithTargetToInvalidEntity()
	{
		TestTarget target = new();
		ActivateTrigger trigger = new();
		trigger.AddTarget(target);
		Level level = new(trigger);
		Assert.Throws<LevelXMLException>(() => level.ToXML());
	}

	[Fact]
	public void TestLevelWithInvalidTriggerActionTagName()
	{
		Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <b i=""0""/>
            </sh>
        </t>
    </triggers>
</levelXML>"));
	}
}
