namespace LevelXML;

/// <summary>
/// This exception represents the fact that an entity would cause the level to freeze on startup.
/// </summary>
public class LevelWouldFreezeOnStartException : LevelXMLException
{
    public LevelWouldFreezeOnStartException() {}
    public LevelWouldFreezeOnStartException(string message) : base(message) {}
    public LevelWouldFreezeOnStartException(string message, Exception innerException) : base(message, innerException) {}
}