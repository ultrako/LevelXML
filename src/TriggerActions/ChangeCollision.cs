using System.Xml.Linq;

namespace LevelXML;

public abstract class ChangeCollision : TriggerAction
{
    internal abstract uint Type { get; }
	public Collision Collision
	{
		get { return (Collision)GetDouble("p0");}
		set { Elt.SetAttributeValue("p0", value);}
	}

	protected ChangeCollision(Collision collision)
	{
		Elt.SetAttributeValue("i", Type);
		Elt.SetAttributeValue("p0", collision);
	}

	protected ChangeCollision(string xml) : this(StrToXElement(xml)) {}

	internal ChangeCollision(XElement e) {
        Elt.SetAttributeValue("i", Type);
		Collision collision = GetDoubleOrNull(e, "p0") ?? 1;
		Elt.SetAttributeValue("p0", collision);
	}
}

/// <summary>
/// This action changes the Entity's collision
/// </summary>
public class ChangeCollision<T> : ChangeCollision, ITriggerAction<T>
{
    internal override uint Type =>
    typeof(T).Name switch
    {
        nameof(Shape) => 7,
        nameof(Group) => 7,
        _ => throw new LevelInvalidException($"You can't have a trigger action changing the collision of a {typeof(T).Name}!", this),
    };

    public ChangeCollision(Collision collision) : base(collision) {}
    
    public ChangeCollision(string xml) : base(xml) {}
    internal ChangeCollision(XElement e) : base(e) {}
}