using Xunit;
using System.Xml.Linq;

namespace LevelXML.Test;

public class TriggerActionTest
{
    [Fact]
    public void InvalidEntityTypeInFromXElement()
    {
        Assert.Throws<InvalidImportException>(() => ITriggerAction<TestEntity>.FromXElement(new XElement("a")));
    }
}