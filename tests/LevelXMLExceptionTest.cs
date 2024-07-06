using Xunit;
using System;
namespace LevelXML.Test;

public class LevelXMLExceptionTest
{
    [Fact]
    public void DefaultConstructorTest()
    {
        LevelXMLException exception = new();
        Assert.Equal("Exception of type 'LevelXML.LevelXMLException' was thrown.",
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