using Xunit;
using System;

namespace HappyWheels.Test;

public class SpikeSetTest
{
    [Fact]
    public void TestDefaultValues()
    {
        SpikeSet spikes = new();
        Assert.Equal(0, spikes.Rotation);
        Assert.True(spikes.Fixed);
        Assert.Equal(20, spikes.Spikes);
        Assert.False(spikes.Sleeping);
    }

    [Fact]
    public void TestSettingRotationAsNaN()
    {
        SpikeSet spikes = new();
        Assert.Throws<LevelXMLException>(() => spikes.Rotation = double.NaN);
    }

    [Fact]
    public void TestSettingSpikesAsNaN()
    {
        SpikeSet spikes = new();
        Assert.Throws<LevelXMLException>(() => spikes.Spikes = double.NaN);
    }
}