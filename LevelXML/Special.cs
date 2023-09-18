using System.Xml.Linq;

namespace HappyWheels;
///<summary>
/// Specials are entities like shapes that have coded behavior besides just collision,
/// like homing mines that follow players or spike sets that stab.
///</summary>
public abstract class Special : Entity, IConvertableToXML
{
	internal abstract uint Type {get;}

	public string ToXML() { return ToXML(mapper: default!); }

	public override double? X
	{
		get { return GetDoubleOrNull("p0"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the special disappear!");
			}
			else { Elt.SetAttributeValue("p0", value); }
		}
	}

	public override double? Y
	{
		get { return GetDoubleOrNull("p1"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new LevelXMLException("This would make the special disappear!");
			}
			else { Elt.SetAttributeValue("p1", value); }
		}
	}

	protected virtual void SetParams(XElement e)
    {
		Elt.SetAttributeValue("t", Type);
        X = GetDoubleOrNull(e, "p0");
        Y = GetDoubleOrNull(e, "p1");
	}
	
	protected Special(XElement e) : base("sp") 
	{
		Elt = new XElement(e.Name.ToString());
	}
}
