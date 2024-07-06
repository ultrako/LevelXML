namespace LevelXML;

/// <summary>
/// The 3 backgrounds a Level can have
/// </summary>
public struct Background
{
    readonly double val;
    public static readonly Background Blank = 0;
    public static readonly Background GreenHills = 1;
    public static readonly Background City = 2;
    /// <summary>
    /// This background will make the level editor still visible when you test the level. 
    /// </summary>
    public static readonly Background Buggy = double.NaN;

    private Background(double val)
    {
        this.val = val;
    }
    public static implicit operator Background(double val)
	{
		return new Background(val);
	}
    public static bool operator ==(Background lhs, Background rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(Background lhs, Background rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is Background t) 
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