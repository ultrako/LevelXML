using System.Xml.Linq;

namespace HappyWheels;
class Trigger : Entity
{
	public static string EditorDefault =
	@"<t x=""0"" y=""0"" w=""100"" h=""100"" a=""0"" b=""1"" t=""1"" r=""1"" sd=""f"" d=""0""/>";
	public override float? x
	{
		get { return GetFloatOrNull("x"); }
		set
		{
			// Having triggers at NaN locations is actually useful;
			// they can still be pointed to by triggers and activate other triggers.
			float val = value ?? float.NaN;
			elt.SetAttributeValue("x", val);
		}
	}
	public override float? y
	{
		get { return GetFloatOrNull("y"); }
		set
		{
			float val = value ?? float.NaN;
			elt.SetAttributeValue("y", val);
		}
	}
	// Targets may come in either the <xml tag> or as a params arg to the constructor
	// If it's in the xml tag, targets have indexes, so it needs the level to be able
	// to have object references (as this class requires)
	public Trigger(params Target<Entity>?[] targets) : this(EditorDefault, targets : targets) {}
	public Trigger(string xml, params Target<Entity>?[] targets) : this(StrToXElement(xml), targets: targets) {}
	public Trigger(XElement xml, params Target<Entity>?[] targets) : base("t", xml.Elements(), targets)
	{
		if (xml.Name.ToString() != "t")
		{
			throw new Exception("Did not give a trigger to the constructor!");
		}
	}
}
