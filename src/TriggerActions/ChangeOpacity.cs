using System.Xml.Linq;

namespace LevelXML;

public abstract class ChangeOpacity : TriggerAction
{
	public const string EditorDefault =
	@"<a p0=""100"" p1=""1"" />";
    internal abstract uint Type { get; }

	public double? Opacity
	{
		get { return GetDoubleOrNull("p0"); }
		set { SetDouble("p0", value ?? 100); }
	}

	public double? Duration
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 1); }
	}

	protected ChangeOpacity(double Opacity, double Duration)
	{
		Elt.SetAttributeValue("i", Type);
		this.Opacity = Opacity;
		this.Duration = Duration;
	}

	protected ChangeOpacity(string xml) : this(StrToXElement(xml)) {}

	internal ChangeOpacity(XElement e)
	{
		Elt.SetAttributeValue("i", Type);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}

/// <summary>
///  This action changes the opacity of this Entity over time
/// </summary>
public class ChangeOpacity<T> : ChangeOpacity, ITriggerAction<T>
{
    internal override uint Type => 
		typeof(T).Name switch
		{
			nameof(Shape) => 3,
			nameof(Group) => 1,
			nameof(TextBox) => 0,
			_ => throw new LevelInvalidException($"You cannot have a trigger action changing the opacity of a {typeof(T).Name}!", this),
		};
		
	public ChangeOpacity(double Opacity, double Duration) : base(Opacity, Duration) {}
	public ChangeOpacity(string xml=EditorDefault) : base(xml) {}
	internal ChangeOpacity(XElement e) : base(e) {}
}