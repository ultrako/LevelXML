namespace HappyWheels;

/// <summary>
/// The 3 different kind of food items
/// </summary>
public struct FoodType
{
    readonly double val;
    public static readonly FoodType Watermelon = 1;
    public static readonly FoodType Pumpkin = 2;
    public static readonly FoodType Pineapple = 3;

    private FoodType(double val)
    {
        if (double.IsNaN(val))
        {
            throw new LevelXMLException("This would make the food disappear!");
        }
        this.val = Math.Clamp(val, 1, 3);
    }
    public static implicit operator FoodType(double val)
	{
		return new FoodType(val);
	}
    public static bool operator ==(FoodType lhs, FoodType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(FoodType lhs, FoodType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is FoodType t) 
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