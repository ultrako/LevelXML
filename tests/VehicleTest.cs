using Xunit;

namespace LevelXML.Test;

public class VehicleTest
{
    [Fact]
    public void TestDefaultValues()
    {
        Vehicle vehicle = new();
        Assert.Equal(VehicleAction.Nothing, vehicle.SpacebarAction);
        Assert.Equal(VehicleAction.Nothing, vehicle.ShiftAction);
        Assert.Equal(VehicleAction.Nothing, vehicle.ControlAction);
        Assert.Equal(1, vehicle.Acceleration);
        Assert.Equal(0, vehicle.LeaningStrength);
        Assert.Equal(GrabbingPose.Limp, vehicle.GrabbingPose);
        Assert.False(vehicle.LockJoints);
    }
}