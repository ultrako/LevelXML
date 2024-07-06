using Xunit;
using System;

namespace LevelXML.Test;

public class SignTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(SignType.Smile != SignType.Radioactive);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(SignType.LeftArrow.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((double.NaN).GetHashCode(), SignType.Empty.GetHashCode());
    }
}