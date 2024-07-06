using Xunit;
using System;

namespace LevelXML.Test;

public class SoccerBallTest
{
    [Fact]
    public void TestDefaultValues()
    {
        SoccerBall soccerBall = new();
        Assert.Equal(@"<sp t=""10"" p0=""0"" p1=""0"" />", 
        soccerBall.ToXML(), ignoreWhiteSpaceDifferences:true);
    }
}