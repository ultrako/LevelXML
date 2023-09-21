namespace HappyWheels;

/// <summary>
/// The 4 kinds of bottles
/// </summary>
public struct BottleType
{
    readonly double val;
    public static readonly BottleType Green = 1;
    public static readonly BottleType Blue = 2;
    public static readonly BottleType Red = 3;
    public static readonly BottleType Yellow = 4;

    private BottleType(double val)
    {
        if (double.IsNaN(val))
        {
            throw new LevelXMLException("This would make the level freeze on start!");
        }
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