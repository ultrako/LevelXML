namespace LevelXML;

/// <summary>
/// The 4 kinds of Bottles
/// </summary>
public struct BottleType
{
    readonly double val;
    public static readonly BottleType Green = 1;
    public static readonly BottleType Blue = 2;
    public static readonly BottleType Red = 3;
    public static readonly BottleType Yellow = 4;
    public static readonly BottleType None = double.NaN;

    private BottleType(double val)
    {
        this.val = Math.Clamp(val, 1, 4);
    }
    public static implicit operator BottleType(double val)
	{
		return new BottleType(val);
	}
    public static bool operator ==(BottleType lhs, BottleType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(BottleType lhs, BottleType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is BottleType t) 
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