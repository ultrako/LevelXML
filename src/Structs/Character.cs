namespace LevelXML;

/// <summary>
/// The 11 characters you can play a level with
/// </summary>
public struct Character
{
    readonly double val;
    public static readonly Character WheelchairGuy = 1;
    public static readonly Character SegwayGuy = 2;
    public static readonly Character IrresponsibleDad = 3;
    public static readonly Character EffectiveShopper = 4;
    public static readonly Character MopedCouple = 5;
    public static readonly Character LawnmowerMan = 6;
    public static readonly Character ExplorerGuy = 7;
    public static readonly Character SantaClaus = 8;
    public static readonly Character PogostickMan = 9;
    public static readonly Character IrresponsibleMom = 10;
    public static readonly Character HelicopterMan = 11;
    public static readonly Character None = double.NaN;

    private Character(double val)
    {
        this.val = Math.Clamp(val, 1, 11);
    }
    public static implicit operator Character(double val)
	{
		return new Character(val);
	}
    public static bool operator ==(Character lhs, Character rhs)
    {
        if (double.IsNaN(lhs.val))
        {
            return double.IsNaN(rhs.val);
        }
        return lhs.val == rhs.val;
    }
    public static bool operator !=(Character lhs, Character rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is Character t) 
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