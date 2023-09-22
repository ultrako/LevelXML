namespace HappyWheels;

/// <summary>
/// The 13 images you can have on a sign
/// </summary>
public struct SignType
{
    readonly double val;
    public static readonly SignType RightArrow = 1;
    public static readonly SignType DownArrow = 2;
    public static readonly SignType LeftArrow = 3;
    public static readonly SignType UpArrow = 4;
    public static readonly SignType Slow = 5;
    public static readonly SignType Stop = 6;
    public static readonly SignType Meteor = 7;
    public static readonly SignType Death = 8;
    public static readonly SignType Pedestrian = 9;
    public static readonly SignType Seesaw = 10;
    public static readonly SignType Radioactive = 11;
    public static readonly SignType Explosive = 12;
    public static readonly SignType Smile = 13;
    public static readonly SignType Empty = double.NaN;

    private SignType(double val)
    {
        this.val = Math.Clamp(val, 1, 13);
    }
    public static implicit operator SignType(double val)
	{
		return new SignType(val);
	}
    public static bool operator ==(SignType lhs, SignType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(SignType lhs, SignType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is SignType t) 
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