using System.Xml.Linq;

namespace LevelXML;
/// <summary>
/// Specials are entities like shapes that have coded behavior besides just collision,
/// like HomingMines that follow players or SpikeSets that stab.
/// </summary>
public abstract class Special : Entity, IConvertibleToXML
{
	internal abstract uint Type {get;}

	public string ToXML() { return ToXML(mapper: default!); }

	public override double X
	{
		get { return GetDoubleOrNull("p0") ?? 0; }
		set
		{
			if (double.IsNaN(value))
			{
				throw new LevelXMLException("This would make the special disappear!");
			}
			else { Elt.SetAttributeValue("p0", value); }
		}
	}

	public override double Y
	{
		get { return GetDoubleOrNull("p1")  ?? 0; }
		set
		{
			if (double.IsNaN(value))
			{
				throw new LevelXMLException("This would make the special disappear!");
			}
			else { Elt.SetAttributeValue("p1", value); }
		}
	}

	protected virtual void SetParams(XElement e)
    {
		Elt.SetAttributeValue("t", Type);
        X = GetDoubleOrNull(e, "p0") ?? double.NaN;
        Y = GetDoubleOrNull(e, "p1") ?? double.NaN;
	}
	
	protected Special(XElement e) : base("sp") 
	{
		Elt = new XElement(e.Name.ToString());
	}
}
