namespace HappyWheels;

/// <summary>
/// The 12 kinds of blade weapons
/// </summary>
public struct WeaponType
{
    readonly double val;
    public static readonly WeaponType Battleaxe = 1;
    public static readonly WeaponType Katana = 2;
    public static readonly WeaponType Sword = 3;
    public static readonly WeaponType Cleaver = 4;
    public static readonly WeaponType Cutlass = 5;
    public static readonly WeaponType Axe = 6;
    public static readonly WeaponType Machete = 7;
    public static readonly WeaponType Spear = 8;
    public static readonly WeaponType Spike = 9;
    public static readonly WeaponType Javelin = 10;
    public static readonly WeaponType Sai = 11;
    public static readonly WeaponType Trident = 12;

    private WeaponType(double val)
    {
        if (double.IsNaN(val))
        {
            throw new LevelXMLException("That would make the blade weapon disappear!");
        }
        this.val = Math.Clamp(val, 1, 12);
    }
    public static implicit operator WeaponType(double val)
	{
		return new WeaponType(val);
	}
    public static bool operator ==(WeaponType lhs, WeaponType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(WeaponType lhs, WeaponType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is WeaponType t) 
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