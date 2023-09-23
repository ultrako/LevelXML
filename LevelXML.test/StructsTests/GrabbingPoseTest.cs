using Xunit;
using System;

namespace HappyWheels.Test;

public class GrabbingPoseTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(GrabbingPose.Limp != GrabbingPose.ArmsForward);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(GrabbingPose.ArmsOverhead.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((3.0).GetHashCode(), GrabbingPose.Hold.GetHashCode());
    }

    [Fact]
    public void TestSettingGrabbingPoseToNaN()
    {
        GrabbingPose vAction = double.NaN;
        Assert.Equal(GrabbingPose.Limp, vAction);
    }
}