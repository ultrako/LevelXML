using System.Xml.Linq;
namespace HappyWheels;

class Level : LevelXMLTag
{
	Level(params LevelXMLTag?[] content) : base("LevelXML", content) {}
}
