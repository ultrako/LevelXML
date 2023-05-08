namespace HappyWheels;
using System.Xml.Linq;

// The action AwakeFromSleep doesn't have any params to it
public class AwakeFromSleep : TriggerAction<Shape>
{
	public AwakeFromSleep()
	{
		elt.SetAttributeValue("i", 0);
	}
}

public class ChangeOpacity : TriggerAction<Shape>
{
	public static string EditorDefault =
	@"<a i=""3"" p0=""100"" p1=""1"" />";
	public float? Opacity
	{
		get { return GetFloatOrNull("p0"); }
		// Test if this is the level editor behavior
		set { elt.SetAttributeValue("p0", Math.Clamp(value ?? 100, 0, 100)); }
	}
	public float? Duration
	{
		get { return GetFloatOrNull("p1"); }
		set { elt.SetAttributeValue("p1", value); }
	}
	public ChangeOpacity() : this(EditorDefault) {}
	public ChangeOpacity(string xml) : this(StrToXElement(xml)) {}
	internal ChangeOpacity(XElement e)
	{
		elt.SetAttributeValue("i", 3);
		Opacity = GetFloatOrNull(e, "p0");
		Duration = GetFloatOrNull(e, "p1");
	}
}
