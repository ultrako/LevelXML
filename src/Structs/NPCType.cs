namespace LevelXML;

/// <summary>
/// The 16 characters you can make as an NPC
/// </summary>
public struct NPCType
{
    readonly double val;
    public static readonly NPCType WheelchairGuy = 1;
    public static readonly NPCType SegwayGuy = 2;
    public static readonly NPCType IrresponsibleDad = 3;
    public static readonly NPCType IrresponsibleSon = 4;
    public static readonly NPCType EffectiveShopper = 5;
    public static readonly NPCType MopedGuy = 6;
    public static readonly NPCType MopedGirl = 7;
    public static readonly NPCType LawnmowerMan = 8;
    public static readonly NPCType ExplorerGuy = 9;
    public static readonly NPCType SantaClaus = 10;
    public static readonly NPCType Elf = 11;
    public static readonly NPCType PogostickMan = 12;
    public static readonly NPCType IrresponsibleMom = 13;
    public static readonly NPCType IrresponsibleDaughter = 14;
    public static readonly NPCType IrresponsibleBaby = 15;
    public static readonly NPCType HelicopterMan = 16;

    private NPCType(double val)
    {
        // Yes, it specifically no longer stays in the LevelXML in import,
        // if I don't throw here it's hard to debug
        if (double.IsNaN(val))
        {
            throw new LevelInvalidException("This would make the NPC disappear!");
        }
        this.val = Math.Clamp(val, 1, 16);
    }
    public static implicit operator NPCType(double val)
	{
		return new NPCType(val);
	}
    public static bool operator ==(NPCType lhs, NPCType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(NPCType lhs, NPCType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is NPCType t) 
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