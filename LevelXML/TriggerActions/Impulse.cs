using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// This action applies an X, Y, and Spin force to an Entity.
/// </summary>
public abstract class Impulse : TriggerAction
{
	public const string EditorDefault =
	@"<a p0=""10"" p1=""-10"" p2=""0""/>";
    internal abstract uint Type { get; }
	public double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set { SetDouble("p0", value ?? 0);}
	}
	public double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 0); }
	}
	public double? Spin
	{
		get { return GetDoubleOrNull("p2"); }
		set { SetDouble("p2", value ?? 0);}
	}
	protected Impulse(double x, double y, double spin)
	{
		Elt.SetAttributeValue("i", Type);
		X = x;
		Y = y;
		Spin = spin;
	}
	protected Impulse(string xml=EditorDefault) : this(StrToXElement(xml)) {}
	internal Impulse(XElement e)
	{
		Elt.SetAttributeValue("i", Type);
		X = GetDoubleOrNull(e, "p0");
		Y = GetDoubleOrNull(e, "p1");
		Spin = GetDoubleOrNull(e, "p2");
	}     
}

public class Impulse<T> : Impulse, ITriggerAction<T>
{
    internal override uint Type => 
		typeof(T).Name switch
		{
			nameof(Shape) => 4,
            nameof(SimpleSpecial) => 1,
			nameof(Group) => 2,
			nameof(NonPlayerCharacter) => 1,
			nameof(GlassPanel) => 2,
			_ => throw new LevelXMLException($"You cannot have a trigger action impulsing a {typeof(T).Name}!"),
		};

	public Impulse(double x, double y, double spin) : base(x, y, spin) {}
	public Impulse(string xml=EditorDefault) : base(xml) {}
	internal Impulse(XElement e) : base(e) {}
}