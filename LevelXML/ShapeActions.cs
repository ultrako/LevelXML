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
	public double? Opacity
	{
		get { return GetDoubleOrNull("p0"); }
		// Test if this is the level editor behavior
		set { elt.SetAttributeValue("p0", Math.Clamp(value ?? 100, 0, 100)); }
	}
	public double? Duration
	{
		get { return GetDoubleOrNull("p1"); }
		set { elt.SetAttributeValue("p1", value); }
	}
	public ChangeOpacity() : this(EditorDefault) {}
	public ChangeOpacity(string xml) : this(StrToXElement(xml)) {}
	internal ChangeOpacity(XElement e)
	{
		elt.SetAttributeValue("i", 3);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}
public class DeleteShape : TriggerAction<Shape>
{
	public DeleteShape()
	{
		elt.SetAttributeValue("i", 6);
	}
}
