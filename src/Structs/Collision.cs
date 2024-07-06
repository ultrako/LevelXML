namespace LevelXML;

/// <summary>
/// What kind of objects a Shape or Group can collide with
/// </summary>
public struct Collision
{
    readonly double val;
    public static readonly Collision Everything = 1;
    public static readonly Collision EverythingButCharacters = 2;
    public static readonly Collision Nothing = 3;
    public static readonly Collision EverythingButSame = 4;
    public static readonly Collision OnlyFixed = 5;
    public static readonly Collision OnlyFixedAndSame = 6;
    public static readonly Collision OnlyCharacters = 7;
    public static readonly Collision NaN = double.NaN;

    private Collision(double val)
    {
        this.val = Math.Clamp(val, 1, 7);
    }
    public static implicit operator Collision(double val)
	{
		return new Collision(val);
	}
    public static bool operator ==(Collision lhs, Collision rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(Collision lhs, Collision rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is Collision t) 
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