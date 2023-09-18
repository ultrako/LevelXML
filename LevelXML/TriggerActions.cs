namespace HappyWheels;

// None of the actions concerning triggers have any parameters

/// <summary>
/// This causes the target trigger to be activated
/// </summary>
public class Activate : TriggerAction<Trigger>
{
	public Activate()
	{
		Elt.SetAttributeValue("i", 0);
	}
}

/// <summary>
/// This causes the target trigger to be disabled, 
/// requiring that another trigger enable it before it can be activated again.
/// </summary>
public class Disable : TriggerAction<Trigger>
{
	public Disable()
	{
		Elt.SetAttributeValue("i", 1);
	}
}

/// <summary>
/// This causes the target trigger to be enabled, so it can be activated in the future.
/// </summary>
public class Enable : TriggerAction<Trigger>
{
	public Enable()
	{
		Elt.SetAttributeValue("i", 2);
	}
}
