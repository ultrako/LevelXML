namespace LevelXML;

/// <summary>
/// The exception thrown when either levelXML given to my library is invalid,
/// "Invalid" means that it would not be accepted by the Happy Wheels import box.
/// </summary>
public class InvalidImportException : LevelXMLException
{
    /// <summary>
    /// The XML tag that was invalid
    /// </summary>
    public string InvalidTag { get; private set; }
    public InvalidImportException(string message, string invalidTag) : base(message) 
    {
        InvalidTag = invalidTag;
    }
}