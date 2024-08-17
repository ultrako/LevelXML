using Xunit;
using System;
namespace LevelXML.Test;

public class ActivateTriggerTest
{
	[Fact]
	public void TriggerTestMinimal()
	{
		// We'll allow NaN position triggers because they can still be pointed to and cause actions
		// We won't allow t to not be set (or set to 0), as that causes the level to freeze on start.
		// And we won't allow r to not be set because then the trigger is useless.
		ActivateTrigger trigger = new(@"<t />");
		Assert.Equal(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""NaN"" sd=""f"" d=""NaN"" />",
			trigger.ToXML(mapper:default!), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void TriggerTestNaN()
	{
		ActivateTrigger trigger = new(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""1"" sd=""NaN"" d=""NaN""/>");
		Assert.Equal(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""1"" sd=""f"" d=""NaN"" />",
			trigger.ToXML(mapper:default!), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void TriggerTestLowValues()
	{
		ActivateTrigger trigger = new(@"<t x=""0"" y=""0"" w=""-1000"" h=""-1000"" a=""0"" b=""-1000"" t=""1"" r=""-1000"" sd=""f"" d=""-1000""/>");
		Assert.Equal(@"<t x=""0"" y=""0"" w=""5"" h=""5"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"" />",
			trigger.ToXML(mapper:default!), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void TriggerTestHighValues()
	{
		// When you don't set the repeat interval on a repeat type 4 trigger,
		// You get a value called "undefined"
		// So now there are 3 kinds of not-existing for float values:
		// not having the attribute set, "NaN", and "undefined".
		// Understandably I just don't want to deal with that shit,
		// plus "undefined" is the same as NaN in every single way
		ActivateTrigger trigger = new(@"<t x=""0"" y=""0"" w=""99999"" h=""99999"" a=""0"" b=""99999"" t=""1"" r=""99999"" sd=""f"" d=""99999""/>");
		Assert.Equal(@"<t x=""0"" y=""0"" w=""5000"" h=""5000"" a=""0"" b=""6"" t=""1"" r=""4"" sd=""f"" i=""NaN"" d=""30"" />",
			trigger.ToXML(mapper:default!), ignoreWhiteSpaceDifferences: true);
	}
	
	
	[Fact]
	public void TriggerWithTargets()
	{
		// Unlike these other tests, importing a trigger with targets doesn't make sense,
		// unless you have a level to map those targets with.
		Rectangle rect = new();
		ActivateTrigger trigger = new(targets: new Target<Shape>(rect, new AwakeFromSleep<Shape>()));
		Assert.Equal(@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
	<sh i=""" + rect.GetHashCode() + @""">
		<a i=""0"" />
	</sh>
</t>",
			trigger.ToXML(mapper: elt => elt.GetHashCode()),
			ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}
	[Fact]
	public void AddTwoTargetsToSameRectangle()
	{
		Rectangle rect = new();
		ActivateTrigger trigger = new();
		trigger.AddTarget(new Target<Shape>(rect, new AwakeFromSleep<Shape>()));
		trigger.AddTarget(new Target<Shape>(rect, new ChangeOpacity<Shape>(0, 0)));
		// We can't assert equality on two TriggerActions because I haven't defined = operators for them
		Assert.IsType<ChangeOpacity<Shape>>(trigger.Targets[0].Actions[1]);
	}
	[Fact]
	public void AddTwoTargetsToSameTrigger()
	{
		ActivateTrigger trigger = new();
		ActivateTrigger otherTrigger = new();
		trigger.AddTarget(new Target<Trigger>(otherTrigger, new Enable()));
		Assert.Throws<LevelInvalidException>(() => trigger.AddTarget(new Target<Trigger>(otherTrigger, new Enable())));
	}
	
	[Fact]
	public void TestTriggerListOperations()
	{
		ActivateTrigger trigger = new();
		Rectangle rect = new();
		Target target1 = new Target<Shape>(rect, new AwakeFromSleep<Shape>());
		Target target2 = new Target<Trigger>(trigger, new Enable());
		Target target3 = new Target<Shape>(rect, new ChangeOpacity<Shape>(50, 0));
		trigger.AddTarget(target1);
		trigger.InsertTarget(0, target2);
		trigger.RemoveTargetAt(1);
		Assert.True(trigger.ContainsTarget(target2));
		trigger.AddTarget(target3);
		Assert.Equal(1, trigger.IndexOfTarget(target3));
		trigger.ClearTargets();
		int size = trigger.TargetCount;
		Assert.Equal(0, size);
		trigger.AddTarget(target1);
		trigger.AddTarget(target2);
		trigger.AddTarget(target3);
		trigger.RemoveTarget(target2);
		int i = 0;
		foreach (Target t in trigger.Targets)
		{
			i += 1;
		}
		Assert.Equal(1, i);
		Assert.NotEmpty(trigger.Targets);
	}
}
