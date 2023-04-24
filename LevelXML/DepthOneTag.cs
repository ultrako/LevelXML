using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
namespace HappyWheels;

// Now, <shapes>, <groups>, etc, tags don't really do everything that XElements do.
// They don't have any attributes, and they're basically just glorified lists.
// We'll have them inherit from LevelXMLTag but implement IList, so that we
// can print them easily, but also use them like lists.

// Just kidding I don't want to implement all of IList
// I'll just pick and choose a few methods on that interface

class DepthOneTag<T> : LevelXMLTag, IList<T> where T : Entity
{
	public int IndexOf(T t)
	{
		int index = 0;
		var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
		foreach (T item in Elements())
		{
			if (comparer.Equals(item, t)) return index;
			index++;
		}
		return -1;
	}
	public T this[int index]
	{
		get { return (T)Elements().Skip(index).First(); }
		set { Elements().Skip(index).First().ReplaceWith(value);} 
	}
	public void Insert(int index, T entity)
	{
		Elements().Skip(index).First().AddAfterSelf(entity);
	}
	public void RemoveAt(int index)
	{
		Elements().Skip(index).First().Remove();
	}
	public void Add(T entity) { Add(entity); }
	public void Clear() { RemoveAll(); }
	public bool Contains(T entity) { return Elements().Contains(entity); }
	public void CopyTo(T[] array, int index)
    {
        for (int i = 0; i < Count; i++)
        {
            array.SetValue(this[i], index++);
        }
    }
	public int Count => Elements().Count();
	public bool Remove(T entity)
	{
		T? found = (T?)Elements().Where(elt => elt == entity).FirstOrDefault();
		if (found is T elt) { elt.Remove(); return true; }
		else { return false; }
	}
	public bool IsReadOnly => false;
	public IEnumerator<T> GetEnumerator() { return (IEnumerator<T>)Elements(); }
	IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator)Elements(); }
	static Dictionary<Type, string> EntityToDepthOneTagName = new()
	{
		{typeof(Shape), "shapes"},
		{typeof(Special), "specials"},
		{typeof(Group), "groups"},
		{typeof(Joint), "joints"},
		{typeof(Trigger), "triggers"},
	};
	DepthOneTag(params T?[] content) : base (EntityToDepthOneTagName[typeof(T)], content) {}
}
