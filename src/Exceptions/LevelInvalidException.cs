namespace LevelXML;

/// <summary>
/// The exception thrown when the level is in an invalid state.
/// "Invalid" means that it would not be accepted by the Happy Wheels import box.
/// </summary>
public class LevelInvalidException : LevelXMLException
{
    /// <summary>
    /// The Entity that is in an invalid state
    /// </summary>
    public IReadOnlyCollection<LevelXMLTag> FaultyEntities { get; private set; }
    public LevelInvalidException(string message, params LevelXMLTag[] faultyEntities) : base(message) 
    {
        FaultyEntities = faultyEntities;
    }
}