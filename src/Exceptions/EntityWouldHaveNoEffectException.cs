using LevelXML;

/// <summary>
/// This exception represents the fact that the faulty tag would
/// categorically have no effect on the level
/// (it still would import and stay in the xml though)
/// </summary>
class TagWouldHaveNoEffectException : LevelXMLException
{
    public LevelXMLTag FaultyTag {get; private set;}
    public TagWouldHaveNoEffectException(string message, Entity faultyTag) : base(message)
    {
        FaultyTag = faultyTag;
    }
}