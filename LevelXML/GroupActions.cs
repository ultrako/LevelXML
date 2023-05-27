using System.Xml.Linq;
namespace HappyWheels;
public class ChangeGroupOpacity : TriggerAction<Group>
{
	public static string EditorDefault =
	@"<a i=""1"" p0=""100"" p1=""1"" />";
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
	public ChangeGroupOpacity(double Opacity, double Duration)
	{
		elt.SetAttributeValue("i", 1);
		this.Opacity = Opacity;
		this.Duration = Duration;
	}
	public ChangeGroupOpacity() : this(EditorDefault) {}
	public ChangeGroupOpacity(string xml) : this(StrToXElement(xml)) {}
	internal ChangeGroupOpacity(XElement e)
	{
		elt.SetAttributeValue("i", 1);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}