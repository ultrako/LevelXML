using Xunit;
using System;

namespace LevelXML.Test;

public class TokenTypeTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(TokenType.Peace != TokenType.Axe);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(TokenType.Skull.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((1.0).GetHashCode(), TokenType.Skull.GetHashCode());
    }
}