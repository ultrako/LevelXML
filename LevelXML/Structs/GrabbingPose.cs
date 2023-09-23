namespace HappyWheels;

/// <summary>
/// The 4 things a vehicle can do when a button is pressed
/// </summary>
public struct GrabbingPose
{
    readonly double val;
    public static readonly GrabbingPose Limp = 0;
    public static readonly GrabbingPose ArmsForward = 1;
    public static readonly GrabbingPose ArmsOverhead = 2;
    public static readonly GrabbingPose Hold = 3;

    private GrabbingPose(double val)
    {
        if (double.IsNaN(val))
        {
            val = 0;
        }
        this.val = Math.Clamp(val, 0, 3);
    }
    public static implicit operator GrabbingPose(double val)
	{
		return new GrabbingPose(val);
	}
    public static bool operator ==(GrabbingPose lhs, GrabbingPose rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(GrabbingPose lhs, GrabbingPose rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is GrabbingPose t) 
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