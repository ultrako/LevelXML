using Xunit;

namespace HappyWheels.Test;

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
        Assert.Throws<LevelXMLException>(() => token.TokenType = double.NaN);
    }
}