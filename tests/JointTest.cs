using Xunit;
using System;
namespace LevelXML.Test;

public class JointTest
{
    [Fact]
    public void JointTestDefault()
    {
        PinJoint joint = new PinJoint();
        Assert.Null(joint.First);
        Assert.Null(joint.Second);
        Assert.Equal(0, joint.X);
        Assert.Equal(0, joint.Y);
        Assert.False(joint.Limit);
        Assert.False(joint.Motorized);
        Assert.False(joint.CollideConnected);
    }

    [Fact]
    public void JointTestMinimal()
    {
        PinJoint pj = new(@"<j />");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />",
        pj.ToXML(mapper:default!), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void JointTestNaN()
    {
        PinJoint pj = new(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""NaN"" ua=""0"" la=""0"" m=""NaN"" tq=""0"" sp=""NaN"" c=""NaN"" />");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""0"" la=""0"" m=""f"" tq=""0"" sp=""NaN"" c=""f"" />",
        pj.ToXML(mapper:default!), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void JointTestHighValues()
    {
        PinJoint pj = new(@"<j t=""0"" ua=""0"" la=""0"" tq=""Infinity"" sp=""Infinity""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""0"" la=""0"" m=""f"" tq=""100000"" sp=""20"" c=""f"" />",
        pj.ToXML(mapper:default!), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void JointTestLowValues()
    {
        PinJoint pj = new(@"<j t=""0"" ua=""0"" la=""0"" tq=""-Infinity"" sp=""-Infinity""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""0"" la=""0"" m=""f"" tq=""-Infinity"" sp=""-20"" c=""f"" />",
        pj.ToXML(mapper:default!), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void PinJointTestDefault()
    {
        PinJoint pj = new();
        Assert.Equal(3, pj.Speed);
        Assert.Equal(90, pj.UpperLimit);
        Assert.Equal(-90, pj.LowerLimit);
        Assert.Equal(50, pj.Torque);
    }
    [Fact]
    public void PinJointTestHighValues()
    {
        PinJoint pj = new();
        pj.UpperLimit = Double.PositiveInfinity;
        pj.LowerLimit = Double.PositiveInfinity;
        pj.Torque = Double.PositiveInfinity;
        Assert.Equal(180, pj.UpperLimit);
        Assert.Equal(-180, pj.LowerLimit);
        Assert.Equal(100000, pj.Torque);
    }

    [Fact]
    public void PinJointTestLowValues()
    {
        PinJoint pj = new();
        pj.UpperLimit = Double.NegativeInfinity;
        pj.LowerLimit = Double.NegativeInfinity;
        pj.Torque = Double.NegativeInfinity;
        Assert.Equal(0, pj.UpperLimit);
        Assert.Equal(-180, pj.LowerLimit);
        Assert.Equal(Double.NegativeInfinity, pj.Torque);
    }

    [Fact]
    public void PinJointTestNaN()
    {
        PinJoint pj = new();
        pj.UpperLimit = Double.NaN;
        pj.LowerLimit = Double.NaN;
        pj.Torque = Double.NaN;
        Assert.Equal(Double.NaN, pj.UpperLimit);
        Assert.Equal(Double.NaN, pj.LowerLimit);
        Assert.Equal(Double.NaN, pj.Torque);
    }

    [Fact]
    public void SlidingJointTestDefault()
    {
        SlidingJoint sj = new();
        Assert.Equal(100, sj.UpperLimit);
        Assert.Equal(-100, sj.LowerLimit);
        Assert.Equal(50, sj.Force);
        Assert.Equal(3, sj.Speed);
        Assert.Equal(90, sj.Angle);
    }

    [Fact]
    public void SlidingJointNaNAngle()
    {
        SlidingJoint sj = new();
        Assert.Throws<LevelXMLException>(() => sj.Angle = double.NaN);
    }

    [Fact]
    public void SlidingJointTestAccessors()
    {
        SlidingJoint sj = new();
        sj.LowerLimit = -10;
        sj.UpperLimit = 10;
        Assert.Equal(-10, sj.LowerLimit);
        Assert.Equal(10, sj.UpperLimit);
    }

    [Fact]
    public void SlidingJointTestHighValues()
    {
        SlidingJoint sj = new();
        sj.UpperLimit = double.PositiveInfinity;
        sj.LowerLimit = double.PositiveInfinity;
        sj.Force = double.PositiveInfinity;
        sj.Speed = double.PositiveInfinity;
        sj.Angle = double.PositiveInfinity;
        Assert.Equal(8000, sj.UpperLimit);
        Assert.Equal(0, sj.LowerLimit);
        Assert.Equal(100000, sj.Force);
        Assert.Equal(50, sj.Speed);
        Assert.Equal(double.PositiveInfinity, sj.Angle);
    }

    [Fact]
    public void SlidingJointTestLowValues()
    {
        SlidingJoint sj = new();
        sj.UpperLimit = double.NegativeInfinity;
        sj.LowerLimit = double.NegativeInfinity;
        sj.Force = double.NegativeInfinity;
        sj.Speed = double.NegativeInfinity;
        sj.Angle = double.NegativeInfinity;
        Assert.Equal(0, sj.UpperLimit);
        Assert.Equal(-8000, sj.LowerLimit);
        Assert.Equal(double.NegativeInfinity, sj.Force);
        Assert.Equal(-50, sj.Speed);
        Assert.Equal(double.NegativeInfinity, sj.Angle);
    }

    [Fact]
    public void SlidingJointTestConstructorWithOneParam()
    {
        Rectangle rect = new();
        SlidingJoint sj = new(rect);
        Assert.Equal(rect, sj.First);
    }
}