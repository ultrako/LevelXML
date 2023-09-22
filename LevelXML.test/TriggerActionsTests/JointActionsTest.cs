using Xunit;
using System;

namespace HappyWheels.Test;

public class JointActionsTest
{
    [Fact]
    public void TestChangeMotorSpeedDefaults()
    {
        ChangeMotorSpeed action = new();
        Assert.Equal(0, action.Speed);
        Assert.Equal(1, action.Duration);
    }

    [Fact]
    public void TestChangeMotorSpeedHighValues()
    {
        ChangeMotorSpeed action = new(double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.Speed);
        Assert.Equal(double.PositiveInfinity, action.Duration);
    }

    [Fact]
    public void TestDeleteSelfJoint()
    {
        DeleteSelfJoint action = new();
        Assert.Equal(@"<a i=""2"" />",
        action.ToXML(), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void TestChangeLimitsDefaultValues()
    {
        ChangeLimits action = new();
        Assert.Equal(90, action.UpperLimit);
        Assert.Equal(-90, action.LowerLimit);
    }

    [Fact]
    public void TestChangeLimitsHighValues()
    {
        ChangeLimits action = new(double.PositiveInfinity, double.PositiveInfinity);
        Assert.Equal(double.PositiveInfinity, action.UpperLimit);
        Assert.Equal(double.PositiveInfinity, action.LowerLimit);
    }
}