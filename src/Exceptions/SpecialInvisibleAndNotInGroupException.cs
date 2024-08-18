using LevelXML;

/// <summary>
/// This exception represents the fact that certain kinds of specials,
/// when interactive and in groups, end up not really in the group on
/// level load, instead moving separately, not being targetable or jointable,
/// and being invisible (though sometimes when they break their parts are visible)
/// </summary>
class SpecialInvisibleAndNotInGroupException : LevelXMLException
{
    public Special FaultySpecial {get; private set;}
    public SpecialInvisibleAndNotInGroupException(string message, Special faultySpecial) : base(message)
    {
        FaultySpecial = faultySpecial;
    }
}