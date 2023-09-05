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
    public void PinJointTestMinimal()
    {
        PinJoint pj = new(@"<j t=""0""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""90"" la=""-90"" m=""f"" tq=""50"" sp=""3"" c=""f"" />",
        pj.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
    [Fact]
    public void PinJointTestNaN()
    {
        PinJoint pj = new(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""NaN"" b2=""NaN"" l=""NaN"" ua=""NaN"" la=""NaN"" m=""NaN"" tq=""NaN"" sp=""NaN"" c=""NaN"" />");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""NaN"" la=""NaN"" m=""f"" tq=""NaN"" sp=""NaN"" c=""f"" />",
        pj.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
    [Fact]
    public void PinJointTestHighValues()
    {
        PinJoint pj = new(@"<j t=""0"" ua=""123456789"" la=""123456789"" tq=""Infinity"" sp=""Infinity""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""180"" la=""-180"" m=""f"" tq=""100000"" sp=""20"" c=""f"" />",
        pj.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
    [Fact]
    public void PinJointTestLowValues()
    {
        PinJoint pj = new(@"<j t=""0"" ua=""-123456789"" la=""-123456789"" tq=""-Infinity"" sp=""-Infinity""/>");
        Assert.Equal(@"<j t=""0"" x=""NaN"" y=""NaN"" b1=""-1"" b2=""-1"" l=""f"" ua=""0"" la=""-180"" m=""f"" tq=""-Infinity"" sp=""-20"" c=""f"" />",
        pj.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
}