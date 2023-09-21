using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// If the special is sleeping, this awakes it from sleep.
/// </summary>
public class AwakeSpecialFromSleep : TriggerAction<SimpleSpecial>
{
    public AwakeSpecialFromSleep()
	{
		SetDouble("i", 0);
	}
}

/// <summary>
///  This action applies an X, Y, and Spin force to a special.
/// </summary>
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

	public ImpulseSpecial(double x, double y, double spin)
	{
		Elt.SetAttributeValue("i", 1);
		X = x;
		Y = y;
		Spin = spin;
	}

	public ImpulseSpecial(string xml=EditorDefault) : this(StrToXElement(xml)) {}

	internal ImpulseSpecial(XElement e)
	{
		Elt.SetAttributeValue("i", 1);
		X = GetDoubleOrNull(e, "p0");
		Y = GetDoubleOrNull(e, "p1");
		Spin = GetDoubleOrNull(e, "p2");
	}
}

/// <summary>
/// This action causes the targeted harpoon to fire.
/// </summary>
public class FireHarpoon : TriggerAction<Harpoon>
{
	public FireHarpoon()
	{
		SetDouble("i", 0);
	}
}

/// <summary>
/// This action prevents a harpoon from firing until activated by another trigger.
/// </summary>
public class DeactivateHarpoon : TriggerAction<Harpoon>
{
	public DeactivateHarpoon()
	{
		SetDouble("i", 1);
	}
}

/// <summary>
/// This action will allow a deactivated harpoon to start firing again.
/// </summary>
public class ActivateHarpoon : TriggerAction<Harpoon>
{
	public ActivateHarpoon()
	{
		SetDouble("i", 2);
	}
}

/// <summary>
///  This action changes the opacity of this textbox over time
/// </summary>
public class ChangeTextBoxOpacity : TriggerAction<TextBox>
{
	public const string EditorDefault =
	@"<a i=""0"" p0=""100"" p1=""1"" />";

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

	public ChangeTextBoxOpacity(double Opacity, double Duration)
	{
		Elt.SetAttributeValue("i", 0);
		this.Opacity = Opacity;
		this.Duration = Duration;
	}

	public ChangeTextBoxOpacity(string xml=EditorDefault) : this(StrToXElement(xml)) {}

	internal ChangeTextBoxOpacity(XElement e)
	{
		Elt.SetAttributeValue("i", 0);
		Opacity = GetDoubleOrNull(e, "p0");
		Duration = GetDoubleOrNull(e, "p1");
	}
}

public class SlideTextBox : TriggerAction<TextBox>
{
	public const string EditorDefault =
	@"<a i=""1"" p0=""1"" p1=""0"" p2=""0""/>";

	/// <summary>
	/// How long it will take the textbox to slide to the specified position
	/// </summary>
	public double? Duration
	{
		get { return GetDoubleOrNull("p0"); }
		set { SetDouble("p0", value ?? 1); }
	}

	/// <summary>
	/// The X coordinate the textbox will slide to
	/// </summary>
	public double? X
	{
		get { return GetDoubleOrNull("p1"); }
		set { SetDouble("p1", value ?? 0); }
	}

	/// <summary>
	/// The Y coordinate the textbox will slide to
	/// </summary>
	public double? Y
	{
		get { return GetDoubleOrNull("p2"); }
		set { SetDouble("p2", value ?? 0); }
	}

	public SlideTextBox(double duration, double x, double y)
	{
		Elt.SetAttributeValue("i", 1);
		Duration = duration;
		X = x;
		Y = y;
	}
	public SlideTextBox(string xml=EditorDefault) : this(StrToXElement(xml)) {}

	internal SlideTextBox(XElement e)
	{
		Elt.SetAttributeValue("i", 1);
		Duration = GetDoubleOrNull(e, "p0");
		X = GetDoubleOrNull(e, "p1");
		Y = GetDoubleOrNull(e, "p2");
	}
}