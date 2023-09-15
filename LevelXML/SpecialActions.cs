using System.Xml.Linq;

namespace HappyWheels;

public class AwakeSpecialFromSleep : TriggerAction<SimpleSpecial>
{
    public AwakeSpecialFromSleep()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

public class ImpulseSpecial : TriggerAction<SimpleSpecial>
{
    public const string EditorDefault =
    @"<a i=""1"" p0=""10"" p1=""-10"" p2=""0""/>";

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

	public ImpulseSpecial() : this(EditorDefault) {}

	public ImpulseSpecial(double x, double y, double spin)
	{
		Elt.SetAttributeValue("i", 1);
		X = x;
		Y = y;
		Spin = spin;
	}

	public ImpulseSpecial(string xml) : this(StrToXElement(xml)) {}

	internal ImpulseSpecial(XElement e)
	{
		Elt.SetAttributeValue("i", 1);
		X = GetDoubleOrNull(e, "p0");
		Y = GetDoubleOrNull(e, "p1");
		Spin = GetDoubleOrNull(e, "p2");
	}
}