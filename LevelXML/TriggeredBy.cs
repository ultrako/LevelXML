namespace HappyWheels;

/// <summary>
///  The reason for which a trigger can be activated
/// </summary>
public struct TriggeredBy
{
    readonly double val;

    /// <summary>
    /// Triggered only by the main character.
    /// </summary>
    public static readonly TriggeredBy MainCharacter = 1;
    /// <summary>
    /// Triggered by any character, including NPCs.
    /// </summary>
    public static readonly TriggeredBy AnyCharacter = 2;
    /// <summary>
    /// Triggered by any non-fixed shape (anything with collision)
    /// </summary>
    public static readonly TriggeredBy Any = 3;
    /// <summary>
    /// Triggered by only the targets attached to this trigger.
    /// </summary>
    public static readonly TriggeredBy Targets = 4;
    /// <summary>
    /// Triggered only by other triggers.
    /// </summary>
    public static readonly TriggeredBy Triggers = 5;
    /// <summary>
    /// Triggered by left-clicking the trigger shape with your mouse.
    /// </summary>
    public static readonly TriggeredBy Click = 6;
    /// <summary>
    /// Triggered by nothing
    /// </summary>
    public static readonly TriggeredBy Nothing = double.NaN;

    private TriggeredBy(double val)
    {
        this.val = Math.Clamp(val, 1, 6);
    }
    public static implicit operator TriggeredBy(double val)
	{
		return new TriggeredBy(val);
	}
    public static bool operator ==(TriggeredBy lhs, TriggeredBy rhs)
    {
        return lhs.val == rhs.val;
    }
    public static bool operator !=(TriggeredBy lhs, TriggeredBy rhs)
    {
        return lhs.val != rhs.val;
    }

    public override bool Equals(Object? that)
		{
			if (that is TriggeredBy t) 
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