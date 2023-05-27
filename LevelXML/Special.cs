namespace HappyWheels;
public abstract class Special : Entity
{
	public abstract uint Type {get;}
	public override double? x
	{
		get { return GetDoubleOrNull("p0"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { elt.SetAttributeValue("p0", value); }
		}
	}
	public override double? y
	{
		get { return GetDoubleOrNull("p1"); }
		set
		{
			if (value is null || double.IsNaN((double)value!))
			{
				throw new Exception("This would make the shape disappear!");
			}
			else { elt.SetAttributeValue("p1", value); }
		}
	}
	protected Special(params object?[] contents) : base("sp", contents) {}
}
