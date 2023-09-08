namespace HappyWheels;
public struct Collision
{
    readonly int val;
    public static readonly Collision Everything = 1;
    public static readonly Collision EverythingButCharacters = 2;
    public static readonly Collision Nothing = 3;
    public static readonly Collision EverythingButSame = 4;
    public static readonly Collision OnlyFixed = 5;
    public static readonly Collision OnlyFixedAndSame = 6;
    public static readonly Collision OnlyCharacters = 7;

    private Collision(int val)
    {
        if (val < 0 || val > 7)
		{
			throw new LevelXMLException("Collision type must be between 0 and 7!");
		} else
        {
            this.val = val;
        }
    }
    public static implicit operator Collision(int val)
	{
		return new Collision(val);
	}
    public static implicit operator Collision(double val)
    {
        return new Collision((int)val);
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
			} else if (that is int val)
            {
                return this.val == val;
            } else
            { 
                return false; 
            }
		}
    public override int GetHashCode() { return val.GetHashCode(); }
}