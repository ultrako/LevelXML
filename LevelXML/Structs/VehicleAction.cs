namespace HappyWheels;

/// <summary>
/// The 4 things a vehicle can do when a button is pressed
/// </summary>
public struct VehicleAction
{
    readonly double val;
    public static readonly VehicleAction Nothing = 0;
    public static readonly VehicleAction BrakeJoints = 1;
    public static readonly VehicleAction FireJets = 2;
    public static readonly VehicleAction ShootArrowGuns = 3;

    private VehicleAction(double val)
    {
        if (double.IsNaN(val))
        {
            val = 0;
        }
        this.val = Math.Clamp(val, 0, 3);
    }
    public static implicit operator VehicleAction(double val)
	{
		return new VehicleAction(val);
	}
    public static bool operator ==(VehicleAction lhs, VehicleAction rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(VehicleAction lhs, VehicleAction rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is VehicleAction t) 
			{
				return this == t; 
			} 
            return false;
		}
    public override int GetHashCode() { return val.GetHashCode(); }
    public override string ToString()
    {
        return val.ToString();
    }
}