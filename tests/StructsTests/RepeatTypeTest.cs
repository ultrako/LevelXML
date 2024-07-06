using Xunit;

namespace LevelXML.Test;

public class RepeatTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(RepeatType.Once != RepeatType.Multiple);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(RepeatType.Once.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((double.NaN).GetHashCode(), RepeatType.Never.GetHashCode());
    }
}