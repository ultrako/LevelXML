namespace HappyWheels;

/// <summary>
/// The exception thrown when either levelXML given to my library is invalid,
/// or the levelXML that my library is trying to produce would be invalid.
/// By invalid, I mean that it would not be accepted by the Happy Wheels import box,
/// would cause the level to crash, or would make an Entity that has 0 effect on the level.
/// </summary>
public class LevelXMLException : Exception
{
    public LevelXMLException() {}
    public LevelXMLException(string message) : base(message) {}
    public LevelXMLException(string message, Exception innerException) : base(message, innerException) {}
}