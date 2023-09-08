namespace HappyWheels;

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
///<summary>
/// A joint connects two non fixed entities together, 
/// specifically shapes, groups, and certain specials.
///</summary>
public abstract class Joint : Entity
{
	internal abstract uint Type {get;}
	public Entity? First {get; set;}
	public Entity? Second {get; set;}
	// Joints with no position are allowed as they do actually affect the game
	public override Double? X
	{
		get
		{
			return GetDoubleOrNull("x");
		}
		set
		{
			Elt.SetAttributeValue("x", value ?? Double.NaN);
		}
	}
	public override Double? Y
	{
		get
		{
			return GetDoubleOrNull("y");
		}
		set
		{
			Elt.SetAttributeValue("y", value ?? Double.NaN);
		}
	}
	public HWBool? Limit
	{
		get
		{
			return GetBoolOrNull("l");
		}
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val == HWBool.NaN) { val = HWBool.False;}
			Elt.SetAttributeValue("l", val);
		}
	}

	public abstract Double? UpperLimit {get; set;}

	public abstract Double? LowerLimit {get; set;}
	public HWBool? Motorized
	{
		get
		{
			return GetBoolOrNull("m");
		}
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val == HWBool.NaN) { val = HWBool.False;}
			Elt.SetAttributeValue("m", val);
		}
	}

	public abstract Double? Speed {get; set;}

	public HWBool? CollideConnected
	{
		get
		{
			return GetBoolOrNull("c");
		}
		set
		{
			HWBool val = value ?? HWBool.False;
			if (val == HWBool.NaN) { val = HWBool.False;}
			Elt.SetAttributeValue("c", val);
		}
	}
	internal override void PlaceInLevel(Func<Entity, int> mapper)
	{
		Elt.SetAttributeValue("b1", getIndexString(mapper, First));
		Elt.SetAttributeValue("b2", getIndexString(mapper, Second));
	}
	private string getIndexString(Func<Entity, int> mapper, Entity? entity)
	{
		string prependType;
		if (entity is null)
		{
			return "-1";
		}
		else if (entity is Shape)
		{
			prependType = String.Empty;
		} 
		else if (entity is Special)
		{
			prependType = "s";
		}
		else if (entity is Group)
		{
			prependType = "g";
		}
		else
		{
			throw new LevelXMLException("Joint is attached to an Entity that can't be jointed!");
		}
		int index = mapper(entity);
		return prependType + index.ToString();
	}
	protected bool isNotJointed(XElement e)
    {
        string? entityOne = GetStringOrNull(e, "b1");
        string? entityTwo = GetStringOrNull(e, "b2");
        return !(couldPointToAnElement(entityOne)
            || couldPointToAnElement(entityTwo));
    }
	private bool couldPointToAnElement(string? jointIndex)
	{
		if (jointIndex == null || jointIndex.Equals("-1"))
		{
			return false;
		}
		char first = jointIndex[0];
		jointIndex = first switch 
		{
			's' => jointIndex.Substring(1, jointIndex.Length-1),
			'g' => jointIndex.Substring(1, jointIndex.Length-1),
			_ => jointIndex
		};
        return int.TryParse(jointIndex, out _);
	}
	virtual protected void SetParams(XElement e, Func<string?, Entity?> reverseMapper)
	{
		Elt.SetAttributeValue("t", Type);
		First = reverseMapper(GetStringOrNull(e, "b1"));
		Second = reverseMapper(GetStringOrNull(e, "b2"));
        X = GetDoubleOrNull(e, "x");
        Y = GetDoubleOrNull(e, "y");
		// These two are just to make sure that b1 and b2 are in the correct place
		Elt.SetAttributeValue("b1", "-1");
		Elt.SetAttributeValue("b2", "-1");
		Limit = GetDoubleOrNull(e, "l");
		UpperLimit = GetDoubleOrNull(e, "ua");
		LowerLimit = GetDoubleOrNull(e, "la");
		Motorized = GetBoolOrNull(e, "m");
	}
	protected Joint(params object?[] contents) : base("j", contents) {}
}
