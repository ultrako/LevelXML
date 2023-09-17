using Xunit;
using System;
namespace HappyWheels.Test;

public class LevelXMLExceptionTest
{
    [Fact]
    public void DefaultConstructorTest()
    {
        LevelXMLException exception = new();
        Assert.Equal("Exception of type 'HappyWheels.LevelXMLException' was thrown.",
            exception.Message);
    }

    [Fact]
    public void InnerExceptionTest()
    {
        Exception inner = new();
        LevelXMLException exception = new("exception", inner);
        Assert.Equal(inner, exception.InnerException);
    }
}