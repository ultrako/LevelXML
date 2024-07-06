using System.Xml.Linq;

namespace LevelXML;

public abstract class ChangeCollision : TriggerAction
{
    internal abstract uint Type { get; }
	public Collision Collision
	{
		get { return (Collision)GetDoubleOrNull(Elt, "p0")!;}
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
		Collision? collision = (Collision?)GetDoubleOrNull(e, "p0");
		if (collision is null)
		{
			throw new LevelXMLException("No collision type on change collision trigger action!");
		}
		else
		{
			Elt.SetAttributeValue("p0", collision);
		}
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
        _ => throw new LevelXMLException($"You can't have a trigger action changing the collision of a {typeof(T).Name}!"),
    };

    public ChangeCollision(Collision collision) : base(collision) {}
    
    public ChangeCollision(string xml) : base(xml) {}
    internal ChangeCollision(XElement e) : base(e) {}
}