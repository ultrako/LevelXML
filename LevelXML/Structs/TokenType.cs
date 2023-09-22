namespace HappyWheels;

/// <summary>
/// The 6 designs you can have on a token
/// </summary>
public struct TokenType
{
    readonly double val;
    public static readonly TokenType Skull = 1;
    public static readonly TokenType Pizza = 2;
    public static readonly TokenType Pentagram = 3;
    public static readonly TokenType Axe = 4;
    public static readonly TokenType Bowling = 5;
    public static readonly TokenType Peace = 6;

    private TokenType(double val)
    {
        if (double.IsNaN(val))
        {
            throw new LevelXMLException("This would make the token disappear!");
        }
        this.val = Math.Clamp(val, 1, 6);
    }
    public static implicit operator TokenType(double val)
	{
		return new TokenType(val);
	}
    public static bool operator ==(TokenType lhs, TokenType rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(TokenType lhs, TokenType rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is TokenType t) 
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