namespace LevelXML;

/// <summary>
/// An exception having to do with a Happy Wheels Level.
/// </summary>
public class LevelXMLException : Exception
{
    public LevelXMLException() {}
    public LevelXMLException(string message) : base(message) {}
    public LevelXMLException(string message, Exception innerException) : base(message, innerException) {}
}