using Xunit;
using System;

namespace HappyWheels.Test;

public class TargetTest {
	[Fact]
	public void TestAwakeFromSleepTarget()
	{
		Rectangle rect = new();
		Target<Shape> target = new(rect, new AwakeShapeFromSleep());
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
		Assert.Throws<LevelXMLException>(() => target.Add(new AwakeGroupFromSleep()));
	}

	[Fact]
	public void TestInsertWrongEntityTypeActionToTarget()
	{
		Target<Shape> target = new(new Rectangle(), new AwakeShapeFromSleep());
		Assert.Throws<LevelXMLException>(() => target[0] = new AwakeGroupFromSleep());
	}

	[Fact]
	public void TestInsertCorrectEntityTypeActionToShapeTarget()
	{
		Target<Shape> target = new(new Rectangle(), new AwakeShapeFromSleep());
		DeleteSelfShape action = new();
		target[0] = action;
		Assert.Equal(action, target[0]);
	}

	[Fact]
	public void TestAddTriggerActionToTarget()
	{
		Target<Trigger> target = new(new ActivateTrigger());
		Enable action = new();
		target.Add(action);
		Assert.Equal(action, target[0]);
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
		AwakeShapeFromSleep action = new();
		target.Add(action);
		Assert.True(target.Remove(action));
		Assert.False(target.Remove(new AwakeGroupFromSleep()));
	}

	[Fact]
	public void TestIndexOf()
	{
		Target<Shape> target = new(new Rectangle());
		AwakeShapeFromSleep action1 = new();
		DeleteSelfShape action2 = new();
		target.Add(action1);
		target.Add(action2);
		Assert.Equal(1, target.IndexOf(action2));
		Assert.Equal(-1, target.IndexOf(new DisableLimits()));
	}
}
