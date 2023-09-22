namespace HappyWheels;

/// <summary>
///  The reason for which a trigger can be activated
/// </summary>
public struct RepeatType
{
    readonly double val;

    /// <summary>
    /// Trigger action occurs only once, then the trigger is deleted.
    /// </summary>
    public static readonly RepeatType Once = 1;
    /// <summary>
    /// Trigger action occurs upon contact with the trigger. It will occur again with new contact after the trigger area is clear of the original triggering body.
    /// </summary>
    public static readonly RepeatType Multiple = 2;
    /// <summary>
    /// Trigger action will occur on a repeated interval until the triggering body leaves the trigger area.
    /// </summary>
    public static readonly RepeatType Continuous = 3;
    /// <summary>
    /// Trigger action will occur on a repeated interval once activated until disabled by other triggers.
    /// </summary>
    public static readonly RepeatType Permanent = 4;
    /// <summary>
    /// The trigger cannot ever be triggered.
    /// </summary>
    public static readonly RepeatType Never = double.NaN;

    private RepeatType(double val)
    {
        this.val = Math.Clamp(val, 1, 4);
    }
    public static implicit operator RepeatType(double val)
	{
		return new RepeatType(val);
	}
    public static bool operator ==(RepeatType lhs, RepeatType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(RepeatType lhs, RepeatType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is RepeatType t) 
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