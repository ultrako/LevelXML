namespace HappyWheels;

// None of the actions concerning triggers have any parameters

public class Activate : TriggerAction<Trigger>
{
	public Activate()
	{
		elt.SetAttributeValue("i", 0);
	}
}

public class Disable : TriggerAction<Trigger>
{
	public Disable()
	{
		elt.SetAttributeValue("i", 1);
	}
}

public class Enable : TriggerAction<Trigger>
{
	public Enable()
	{
		elt.SetAttributeValue("i", 2);
	}
}
