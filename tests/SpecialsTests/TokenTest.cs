using Xunit;

namespace LevelXML.Test;

public class TokenTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Token token = new();
        Assert.Equal(TokenType.Skull, token.TokenType);
    }

    [Fact]
    public void TestSettingTokenTypeAsNaN()
    {
        Token token = new();
        token.TokenType = double.NaN;
    }
}