namespace HappyWheels;

/// <summary>
/// The 2 kinds of skins a cannon can have
/// (and an invisible cannon)
/// </summary>
public struct CannonType
{
    readonly double val;
    public static readonly CannonType Circus = 1;
    public static readonly CannonType Metal = 2;
    public static readonly CannonType Invisible = double.NaN;

    private CannonType(double val)
    {
        this.val = Math.Clamp(val, 1, 2);
    }
    public static implicit operator CannonType(double val)
	{
		return new CannonType(val);
	}
    public static bool operator ==(CannonType lhs, CannonType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(CannonType lhs, CannonType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is CannonType t) 
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