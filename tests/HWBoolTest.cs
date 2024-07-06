using Xunit;
using System;

namespace LevelXML.Test;
public class HWBoolTest
{
    [Fact]
    public void HWBoolImplicitBoolConversionNanIsFalse()
    {
        Assert.False(LevelXMLTag.HWBool.NaN);
    }

    [Fact]
    public void HWBoolNotEquals()
    {
        Assert.True(LevelXMLTag.HWBool.False != LevelXMLTag.HWBool.True);
    }

    [Fact]
    public void HWBoolHashCode()
    {
        Assert.Equal(LevelXMLTag.HWBool.True.GetHashCode(), true.GetHashCode());
    }

    [Fact]
    public void HWBoolEqualsToBool()
    {
        Assert.True(LevelXMLTag.HWBool.False.Equals(false));
    }

    [Fact]
    public void HWBoolNotEqualToInt()
    {
        bool b = LevelXMLTag.HWBool.True.Equals(0);
        Assert.False(b);
    }
}