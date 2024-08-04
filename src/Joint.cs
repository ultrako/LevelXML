using System.Xml.Linq;

namespace LevelXML;
/// <summary>
/// A Joint connects two non fixed entities together, 
/// specifically shapes, groups, and certain specials.
/// </summary>
public abstract class Joint : Entity
{
	internal abstract uint Type {get;}
	private Entity? first;
	private Entity? second;

	/// <summary>
	///  The first Entity this Joint is jointed to.
	///  Null means it's jointed to the background.
	/// </summary>
	public Entity? First
	{
		get { return first;}
		set
		{
			first = value;
		}
	}

	/// <summary>
	///  The second Entity this Joint is jointed to.
	///  Null means it's jointed to the background.
	/// </summary>
	public Entity? Second
	{
		get { return second;}
		set
		{
			second = value;
		}
	}

	// Joints with NaN position are allowed as they do actually affect the game
	public override double X
	{
		get
		{
			return GetDouble("x");
		}
		set
		{
			SetDouble("x", value);
		}
	}

	public override double Y
	{
		get
		{
			return GetDouble("y");
		}
		set
		{
			SetDouble("y", value);
		}
	}

	/// <summary>
	///  Whether or not the Joint has limits in its motion
	/// </summary>
	public HWBool Limit
	{
		get
		{
			return GetBool("l");
		}
		set
		{
			HWBool val = value;
			if (val == HWBool.NaN) { val = HWBool.False;}
			Elt.SetAttributeValue("l", val);
		}
	}

	/// <summary>
	///  The upper limit of motion for the Joint.
	///  It's a maximum angle for PinJoints and a maximum distance for SlidingJoints.
	/// </summary>
	public abstract double UpperLimit {get; set;}

	/// <summary>
	///  The lower limit of motion for the Joint.
	///  It's a minimum angle for PinJoints and a minimum distance for SlidingJoints.
	/// </summary>
	public abstract double LowerLimit {get; set;}

	/// <summary>
	/// Whether or not the Joint moves on its own based on its speed and force.
	/// </summary>
	public HWBool Motorized
	{
		get
		{
			return GetBool("m");
		}
		set
		{
			HWBool val = value;
			if (val == HWBool.NaN) { val = HWBool.False;}
			Elt.SetAttributeValue("m", val);
		}
	}

	/// <summary>
	///  The top speed at which a Joint will move
	/// </summary>
	public abstract double Speed {get; set;}

	/// <summary>
	///  Whether or not the two objects that a Joint connects, can collide with one another.
	/// </summary>
	public HWBool CollideConnected
	{
		get
		{
			return GetBool("c");
		}
		set
		{
			HWBool val = value;
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
		string prependType = String.Empty;
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
		return !(jointIndex == null || jointIndex.Equals("-1"));
	}

	virtual protected void SetParams(XElement e, Func<string?, Entity?> reverseMapper)
	{
		Elt.SetAttributeValue("t", Type);
		First = reverseMapper(GetStringOrNull(e, "b1"));
		Second = reverseMapper(GetStringOrNull(e, "b2"));
        X = GetDoubleOrNull(e, "x") ?? double.NaN;
        Y = GetDoubleOrNull(e, "y") ?? double.NaN;
		// Setting these two now just to make sure that b1 and b2 are in the correct place
		Elt.SetAttributeValue("b1", "-1");
		Elt.SetAttributeValue("b2", "-1");
		Limit = GetBoolOrNull(e, "l") ?? false;
		// This default value is different depending on whether it is a sliding joint or a pin joint
		double lim = Type switch { 0 => 90, _ => 100 };
		string upperLim = Type switch { 0 => "ua", _ => "ul"};
		string lowerLim = Type switch { 0 => "la", _ => "ll"};
		UpperLimit = GetDoubleOrNull(e, upperLim) ?? lim;
		LowerLimit = GetDoubleOrNull(e, lowerLim) ?? -lim;
		Motorized = GetBoolOrNull(e, "m") ?? false;
	}
	
	protected Joint(XElement e) : base("j") 
	{
		Elt = new XElement(e.Name.ToString());
	}
}
