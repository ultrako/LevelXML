public struct Collision
{
    readonly int val;
    public static readonly Everything = 1;
    public static readonly EverythingButCharacters = 2;
    public static readonly Nothing = 3;
    public static readonly EverythingButSame = 4;
    public static readonly OnlyFixed = 5;
    public static readonly OnlyFixedAndSame = 6;
    public static readonly OnlyCharacters = 7;

    private Collision(int val)
    {
        this.val = val;
    }
    public static implicit operator Collision(int val)
	{
		return Collision(val);
	}
    public static implicit operator Collision(double val)
    {
        return Collision((int)val);
    }
    public static bool operator ==(Collision lhs, Collision rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(Collision lhs, Collision rhs)
    {
        return lhs.val != rhs.val;
    }
    public override int GetHashCode() { return val.GetHashCode(); }
}