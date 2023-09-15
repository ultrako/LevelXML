namespace HappyWheels;

/// <summary>
/// These are all of the classes that can directly be converted to XML,
/// without needing any more context like target IDs or art vertex IDs
/// </summary>
public interface IConvertableToXML
{
    ///<summary>
	/// This converts this levelXMLTag object into the valid levelXML
	/// that represents this object
	///</summary>
    abstract string ToXML();
}