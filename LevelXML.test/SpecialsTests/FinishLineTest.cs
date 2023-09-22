using Xunit;

namespace HappyWheels;

public class FinishLineTest
{
    [Fact]
    public void TestDefault()
    {
        FinishLine finishLine = new();
        Assert.Equal(@"<sp t=""9"" p0=""0"" p1=""0"" />",
        finishLine.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
}