using Xunit;
using System;
using System.Xml.Linq;

namespace LevelXML.Test;

public class TriggerActionTest
{
    [Fact]
    public void InvalidEntityTypeInFromXElement()
    {
        Assert.Throws<LevelXMLException>(() => ITriggerAction<TestEntity>.FromXElement(new XElement("a")));
    }
}