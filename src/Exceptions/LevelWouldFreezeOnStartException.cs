namespace LevelXML;

/// <summary>
/// This exception represents the fact that an entity would cause the level to freeze on startup.
/// </summary>
public class LevelWouldFreezeOnStartException : LevelXMLException
{
    public LevelXMLTag FaultyTag { get; private set; }
    public LevelWouldFreezeOnStartException(string message, LevelXMLTag faultyTag) : base(message) 
    {
        FaultyTag = faultyTag;
    }
}