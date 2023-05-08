using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
namespace HappyWheels;

// Now, <shapes>, <groups>, etc, tags don't really do everything that XElements do.
// XElementhey don't have any attributes, and they're basically just glorified lists.
// We'll have them inherit from LevelXMLXElementag but implement IList, so that we
// can print them easily, but also use them like lists.

// Just kidding I don't want to implement all of IList
// I'll just pick and choose a few methods on that interface

class XElementAsList: XElement, IList<XElement>
{
	public int IndexOf(XElement t)
	{
		int index = 0;
		var comparer = EqualityComparer<XElement>.Default; // or pass in as a parameter
		foreach (XElement item in Elements())
		{
			if (comparer.Equals(item, t)) return index;
			index++;
		}
		return -1;
	}
	public XElement this[int index]
	{
		get { return (XElement)Elements().Skip(index).First(); }
		set { Elements().Skip(index).First().ReplaceWith(value);} 
	}
	public void Insert(int index, XElement entity)
	{
		Elements().Skip(index).First().AddAfterSelf(entity);
	}
	public void RemoveAt(int index)
	{
		Elements().Skip(index).First().Remove();
	}
	public void Add(XElement entity) { Add(entity); }
	public void Clear() { RemoveAll(); }
	public bool Contains(XElement entity) { return Elements().Contains(entity); }
	public void CopyTo(XElement[] array, int index)
    {
        for (int i = 0; i < Count; i++)
        {
            array.SetValue(this[i], index++);
        }
    }
	public int Count => Elements().Count();
	public bool Remove(XElement entity)
	{
		XElement? found = (XElement?)Elements().Where(elt => elt == entity).FirstOrDefault();
		if (found is XElement elt)  { Remove(elt); return true; }
		else { return false; }
	}
	public bool IsReadOnly => false;
	public IEnumerator<XElement> GetEnumerator() { return (IEnumerator<XElement>)Elements(); }
	IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator)Elements(); }
	XElementAsList(String name, params XElement?[] content) : base (name, content) {}
}
