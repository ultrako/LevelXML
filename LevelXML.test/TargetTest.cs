using Xunit;
using System;

namespace HappyWheels.Test;

public class TargetTest {
	[Fact]
	public void TestAwakeFromSleepTarget()
	{
		Rectangle rect = new();
		Target<Shape> target = new(rect, new AwakeFromSleep<Shape>());
		Assert.Equal(@"<sh i=""" + rect.GetHashCode() + @""">
 <a i=""0"" />
</sh>",
			target.ToXML(mapper: entity => entity.GetHashCode()),
			ignoreWhiteSpaceDifferences: true, ignoreLineEndingDifferences: true);
	}

	[Fact]
	public void TestAddWrongEntityTypeActionToTarget()
	{
		Target<Shape> target = new(new Rectangle());
		Assert.Throws<LevelXMLException>(() => target.AddAction(new AwakeFromSleep<Group>()));
	}

	[Fact]
	public void TestAddTriggerActionToTarget()
	{
		Target<Trigger> target = new(new ActivateTrigger());
		Enable action = new();
		target.AddAction(action);
		Assert.Equal(action, target.Actions[0]);
	}

	[Fact]
	public void TestConstructTriggerTargetWithTwoActions()
	{
		Assert.Throws<LevelXMLException>(() => new Target<Trigger>(new ActivateTrigger(), new Enable(), new Disable()));
	}

	[Fact]
	public void TestRemoveAction()
	{
		Target<Shape> target = new(new Rectangle());
		AwakeFromSleep<Shape> action = new();
		target.AddAction(action);
		Assert.True(target.RemoveAction(action));
		Assert.False(target.RemoveAction(new AwakeFromSleep<Group>()));
	}

	[Fact]
	public void TestIndexOf()
	{
		Target<Shape> target = new(new Rectangle());
		AwakeFromSleep<Shape> action1 = new();
		DeleteSelf<Shape> action2 = new();
		target.AddAction(action1);
		target.AddAction(action2);
		Assert.Equal(1, target.IndexOfAction(action2));
		Assert.Equal(-1, target.IndexOfAction(new DisableLimits()));
	}
}
