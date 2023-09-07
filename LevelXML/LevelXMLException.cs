namespace HappyWheels;

public class LevelXMLException : Exception
{
    public LevelXMLException() {}
    public LevelXMLException(string message) : base(message) {}
    public LevelXMLException(string message, Exception innerException) : base(message, innerException) {}
}