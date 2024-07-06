using Xunit;
using System;

namespace LevelXML.Test;

public class VehicleActionTest
{
    [Fact]
    public void TestNotEqualsOperator()
    {
        Assert.True(VehicleAction.BrakeJoints != VehicleAction.ShootArrowGuns);
    }

    [Fact]
    public void TestEqualsMethod()
    {
        Assert.False(VehicleAction.FireJets.Equals(1));
    }

    [Fact]
    public void TestHashCode()
    {
        Assert.Equal((0).GetHashCode(), VehicleAction.Nothing.GetHashCode());
    }

    [Fact]
    public void TestSettingVehicleActionToNaN()
    {
        VehicleAction vAction = double.NaN;
        Assert.Equal(VehicleAction.Nothing, vAction);
    }
}