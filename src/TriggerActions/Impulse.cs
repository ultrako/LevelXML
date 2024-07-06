using System.Xml.Linq;

namespace LevelXML;

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

/// <summary>
/// This action applies an X, Y, and Spin force to an Entity.
/// </summary>
public class Impulse<T> : Impulse, ITriggerAction<T>
{
    internal override uint Type => 
		typeof(T).Name switch
		{
			nameof(Shape) => 4,
			nameof(Group) => 2,
			nameof(IBeam) => 1,
			nameof(Rail) => 1,
			nameof(SpikeSet) => 1,
			nameof(Television) => 1,
			nameof(Toilet) => 1,
			nameof(TrashCan) => 1,
			nameof(Van) => 1,
			nameof(Chair) => 1,
			nameof(Boombox) => 1,
			nameof(DinnerTable) => 1,
			nameof(NonPlayerCharacter) => 1,
			nameof(BladeWeapon) => 1,
			nameof(Chain) => 1,
			nameof(Log) => 1,
			nameof(Food) => 1,
			nameof(Bottle) => 1,
			nameof(Meteor) => 1,
			nameof(GlassPanel) => 2,
			_ => throw new LevelXMLException($"You cannot have a trigger action impulsing a {typeof(T).Name}!"),
		};

	public Impulse(double x, double y, double spin) : base(x, y, spin) {}
	public Impulse(string xml=EditorDefault) : base(xml) {}
	internal Impulse(XElement e) : base(e) {}
}