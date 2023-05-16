using Xunit;
using System;
namespace HappyWheels.test;

public class TriggerTest
{
	[Fact]
	public void TriggerTestBlank()
	{
		Assert.Throws<Exception>(() => new Trigger("<t />"));
	}
	[Fact]
	public void TriggerTestMinimal()
	{
		// We'll allow NaN position triggers because they can still be pointed to and cause actions
		// We won't allow t to not be set (or set to 0), as that causes the level to freeze on start.
		// And we won't allow r to not be set because then the trigger is useless.
		Trigger trigger = new(@"<t t=""1"" r=""1"" />");
		Assert.Equal(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""1"" sd=""f"" d=""NaN"" />",
			trigger.ToString(), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void TriggerTestNaN()
	{
		Trigger trigger = new(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""1"" sd=""NaN"" d=""NaN""/>");
		Assert.Equal(@"<t x=""NaN"" y=""NaN"" w=""NaN"" h=""NaN"" a=""NaN"" b=""NaN"" t=""1"" r=""1"" sd=""f"" d=""NaN"" />",
			trigger.ToString(), ignoreWhiteSpaceDifferences: true);
	}
	[Fact]
	public void TriggerTestLowValues()
	{
		Trigger trigger = new(@"<t x=""0"" y=""0"" w=""-1000"" h=""-1000"" a=""0"" b=""-1000"" t=""1"" r=""-1000"" sd=""f"" d=""-1000""/>");
		Assert.Equal(@"<t x=""0"" y=""0"" w=""5"" h=""5"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"" />",
			trigger.ToString(), ignoreWhiteSpaceDifferences: true);
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
		Trigger trigger = new(@"<t x=""0"" y=""0"" w=""99999"" h=""99999"" a=""0"" b=""99999"" t=""1"" r=""99999"" sd=""f"" d=""99999""/>");
		Assert.Equal(@"<t x=""0"" y=""0"" w=""5000"" h=""5000"" a=""0"" b=""6"" t=""1"" r=""4"" sd=""f"" i=""NaN"" d=""30"" />",
			trigger.ToString(), ignoreWhiteSpaceDifferences: true);
	}
	// Being able to set the mapper in ToString makes no sense as an interface,
	// so we shouldn't test it.
	// Move the test up to LevelTest.cs
	/*
	[Fact]
	public void TriggerWithTargets()
	{
		// Unlike these other tests, importing a trigger with targets doesn't make sense,
		// unless you have a level to map those targets with.
		Rectangle rect = new();
		Trigger trigger = new(targets: new Target<Shape>(rect, new AwakeFromSleep()));
		Assert.Equal(@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0"">
	<sh i=""" + rect.GetHashCode() + @""">
		<a i=""0"" />
	</sh>
</t>",
			trigger.ToString(mapper: elt => elt.GetHashCode()),
			ignoreWhiteSpaceDifferences:true, ignoreLineEndingDifferences: true);
	}
	*/
}
