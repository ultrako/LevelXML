using Xunit;
using System;
namespace HappyWheels.Test;

public class JointTest
{
    [Fact]
    public void PinJointTestBlank()
    {
		Assert.Throws<Exception>(() => new PinJoint("<j />"));
    }

    [Fact]
    public void JointTestDefault()
    {
        PinJoint joint = new PinJoint();
        Assert.Null(joint.First);
        Assert.Null(joint.Second);
        Assert.Equal(0, joint.X);
        Assert.Equal(0, joint.Y);
        Assert.Equal(false, joint.Limit);
        Assert.Equal(false, joint.Motorized);
        Assert.Equal(false, joint.CollideConnected);
    }

    [Fact]
    public void JointTestMinimal()
    {
        PinJoint pj = new(@"<j t=""0""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />",
        pj.ToXML(mapper:default!), ignoreWhiteSpaceDifferences:true);
    }

    [Fact]
    public void JointTestNaN()
    {
        PinJoint pj = new(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""NaN"" b2=""NaN"" l=""NaN"" ua=""0"" la=""0"" m=""NaN"" tq=""0"" sp=""NaN"" c=""NaN"" />");
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
}