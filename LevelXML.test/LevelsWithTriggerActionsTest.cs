using Xunit;
using System;
using System.Linq;

namespace HappyWheels.Test;

public class LevelsWithTriggerActionsTest
{
    [Fact]
    public void ParseLevelWithFixShape()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""1""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<Fix<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithNonfixShape()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""2""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<Nonfix<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithChangeShapeOpacity()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""3"" p0=""100"" p1=""1""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<ChangeOpacity<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithImpulseShape()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""4"" p0=""10"" p1=""-10"" p2=""0""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<Impulse<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDeleteShape()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""5""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<DeleteShapes<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDeleteSelfShape()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""6""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<DeleteSelf<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithChangeShapeCollision()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""7"" p0=""1""/>
            </sh>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Shape> target = (Target<Shape>)trigger.Targets[0];
        Assert.IsType<ChangeCollision<Shape>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithInvalidShapeActionID()
    {
        Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""542"" p1=""5338"" p2=""300"" p3=""100"" p4=""0"" p5=""t"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <triggers>
        <t x=""314"" y=""5223"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sh i=""0"">
                <a i=""8""/>
            </sh>
        </t>
    </triggers>
</levelXML>"));
    }

    [Fact]
    public void ParseLevelWithImpulseVan()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""0"" p0=""483"" p1=""5312"" p2=""0"" p3=""f"" p4=""t""/>
    </specials>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""1"" p0=""10"" p1=""-10"" p2=""0""/>
            </sp>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Van> target = (Target<Van>)trigger.Targets[0];
        Assert.IsType<Impulse<Van>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithInvalidSpecialActionID()
    {
        Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <specials>
        <sp t=""0"" p0=""483"" p1=""5312"" p2=""0"" p3=""f"" p4=""t""/>
    </specials>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <sp i=""0"">
                <a i=""2""/>
            </sp>
        </t>
    </triggers>
</levelXML>"));
    }

    [Fact]
    public void ParseLevelWithChangeGroupOpacity()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""1"" p0=""100"" p1=""1""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<ChangeOpacity<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithImpulseGroup()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""2"" p0=""10"" p1=""-10"" p2=""0""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<Impulse<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithFixGroup()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""3""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<Fix<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithNonfixGroup()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""4""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<Nonfix<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDeleteShapeGroup()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""5""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<DeleteShapes<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDeleteSelfGroup()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""6""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<DeleteSelf<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithChangeGroupCollision()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""7"" p0=""1""/>
            </g>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Group> target = (Target<Group>)trigger.Targets[0];
        Assert.IsType<ChangeCollision<Group>>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithInvalidGroupActionID()
    {
        Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <groups>
        <g x=""519"" y=""5330"" r=""0"" ox=""-488"" oy=""-5344"" s=""f"" f=""f"" o=""100"" im=""f"" fr=""f"">
            <sh t=""0"" p0=""488"" p1=""5344"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
        </g>
    </groups>
    <triggers>
        <t x=""300"" y=""5236"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <g i=""0"">
                <a i=""8""/>
            </g>
        </t>
    </triggers>
</levelXML>"));
    }

    [Fact]
    public void ParseLevelWithChangeMotorSpeed()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""519"" p1=""5330"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <joints>
        <j t=""0"" x=""458"" y=""5333"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""1"" p0=""0"" p1=""1""/>
            </j>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Joint> target = (Target<Joint>)trigger.Targets[0];
        Assert.IsType<ChangeMotorSpeed>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDeleteSelfJoint()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""519"" p1=""5330"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <joints>
        <j t=""0"" x=""458"" y=""5333"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""2""/>
            </j>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Joint> target = (Target<Joint>)trigger.Targets[0];
        Assert.IsType<DeleteSelfJoint>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithDisableLimits()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""519"" p1=""5330"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <joints>
        <j t=""0"" x=""458"" y=""5333"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""3""/>
            </j>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Joint> target = (Target<Joint>)trigger.Targets[0];
        Assert.IsType<DisableLimits>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithChangeLimits()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""519"" p1=""5330"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <joints>
        <j t=""0"" x=""458"" y=""5333"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""4"" p0=""90"" p1=""-90""/>
            </j>
        </t>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Joint> target = (Target<Joint>)trigger.Targets[0];
        Assert.IsType<ChangeLimits>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithInvalidJointActionID()
    {
        Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <shapes>
        <sh t=""0"" p0=""519"" p1=""5330"" p2=""300"" p3=""100"" p4=""0"" p5=""f"" p6=""f"" p7=""1"" p8=""4032711"" p9=""-1"" p10=""100"" p11=""1""/>
    </shapes>
    <joints>
        <j t=""0"" x=""458"" y=""5333"" b1=""0"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f""/>
    </joints>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <j i=""0"">
                <a i=""5""/>
            </j>
        </t>
    </triggers>
</levelXML>"));
    }

    [Fact]
    public void ParseLevelWithDisable()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <t i=""1"">
                <a i=""1""/>
            </t>
        </t>
        <t x=""496"" y=""5325"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Trigger> target = (Target<Trigger>)trigger.Targets[0];
        Assert.IsType<Disable>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithEnable()
    {
        Level level = new(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <t i=""1"">
                <a i=""2""/>
            </t>
        </t>
        <t x=""496"" y=""5325"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>
    </triggers>
</levelXML>");
        ActivateTrigger trigger = (ActivateTrigger)level.Triggers[0];
        Target<Trigger> target = (Target<Trigger>)trigger.Targets[0];
        Assert.IsType<Enable>(target.Actions[0]);
    }

    [Fact]
    public void ParseLevelWithInvalidTriggerActionID()
    {
        Assert.Throws<LevelXMLException>(() => new Level(@"<levelXML>
    <info v=""1.95"" x=""300"" y=""5100"" c=""1"" f=""f"" h=""f"" bg=""0"" bgc=""16777215"" e=""1""/>
    <triggers>
        <t x=""385"" y=""5170"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
            <t i=""1"">
                <a i=""3""/>
            </t>
        </t>
        <t x=""496"" y=""5325"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>
    </triggers>
</levelXML>"));
    }
}