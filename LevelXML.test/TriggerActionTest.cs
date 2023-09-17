using Xunit;
using System;
using System.Xml.Linq;

namespace HappyWheels.Test;

public class TriggerActionTest
{
    [Fact]
    public void InvalidEntityTypeInFromXElement()
    {
        Assert.Throws<LevelXMLException>(() => TriggerAction<TestEntity>.FromXElement(new XElement("a")));
    }
}