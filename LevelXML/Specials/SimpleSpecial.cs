using System.Xml.Linq;

namespace HappyWheels;

/// <summary>
/// SimpleSpecials are those specials that are simply a nonfixed polygon with a texture on it
/// Important to know is that all of these can only have two trigger actions: awaking from sleep, or impulsing
/// </summary>
public abstract class SimpleSpecial : Special
{
    internal SimpleSpecial(XElement e) : base(e) {}
}