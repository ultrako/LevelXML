using System.Xml.Linq;
namespace HappyWheels;

/// <summary>
/// The Level class represents a Happy Wheels level.
/// </summary>
/// <remarks>
/// As this extends LevelXMLTag,
/// ToString() will print out the LevelXML that represents this level,
/// ready to be pasted into the happy wheels import box.
/// </remarks>
public class Level : LevelXMLTag
{
	///<summary>
	/// The info tag has information about the character and the background of the level
	///</summary>
	public Info Info;
	public DepthOneTag<Shape>? Shapes;
	public DepthOneTag<Special>? Specials;
	public DepthOneTag<Group>? Groups;
	public DepthOneTag<Joint>? Joints;
	public DepthOneTag<Trigger>? Triggers;
	internal override void PlaceInLevel(Func<Entity, int> _)
	{
		elt.RemoveNodes();
		elt.Add(Info.elt);
		foreach (DepthOneTag? tag in new List<DepthOneTag?> {Shapes, Specials, Groups, Joints, Triggers})
		{
			if (tag is not null)
			{
				tag.PlaceInLevel(mapper);
				elt.Add(tag.elt);
			}
		}
	}
	private Dictionary<string, Type> NameToEntityType = new()
	{
		{"sh", typeof(Shape)},
		{"sp", typeof(Special)},
		{"g", typeof(Group)},
		{"j", typeof(Joint)},
		{"t", typeof(Trigger)},
	};
	private DepthOneTag? TypeToDepthOneTag(Type t)
	{
		return t.Name switch {
			nameof(Shape) => Shapes,
			nameof(Special) => Specials,
			nameof(Group) => Groups,
			nameof(Joint) => Joints,
			nameof(Trigger) => Triggers,
			_ => throw new Exception($"Levels don't hold the type {t.Name}!"),
		};
	}
	private int mapper(Entity e)
	{
		return e switch
		{
			Shape s => (Shapes ?? new DepthOneTag<Shape>()).IndexOf(s),
			Special s => (Specials ?? new DepthOneTag<Special>()).IndexOf(s),
			Group s => (Groups ?? new DepthOneTag<Group>()).IndexOf(s),
			Joint s => (Joints ?? new DepthOneTag<Joint>()).IndexOf(s),
			Trigger s => (Triggers ?? new DepthOneTag<Trigger>()).IndexOf(s),
			_ => -1,
		};
	}
	private AutoResetEvent depthOneTagsReady = new(false);
	private Entity reverseMapper(XElement e)
	{
		int index = Int32.Parse(e.Attribute("i")!.Value)!;
		Type entityType = NameToEntityType[e.Name.ToString()];
		//Console.WriteLine($"Trying to get the {index}th {entityType}");
		// Wait until the class sets that the tags are ready to index through
		// This is needed because the constructors of these entities use the reverse mapper,
		// but we haven't made a depthOneTag until we've constructed all of its Entities.
		// The consequence is that the constructors for Entities can't wait for
		// this function to finish.
		depthOneTagsReady.WaitOne();
		DepthOneTag? lst = TypeToDepthOneTag(entityType);
		if (lst is null)
		{
			throw new Exception("Tried to index into a depth one tag we didn't construct!");
		} else
		{
			return lst.get(index);
		}
	}
	/// <summary>
	/// This constructor makes a Level from an Info tag and several Entities 
	/// (which are shapes, specials, groups, joints, and triggers).
	/// If no info tag is supplied, then the default one from opening the level
	/// editor and just hitting save is set.
	/// </summary>
	public Level(Info info=default!, params Entity[] entities) :
		this(info : info,
			shapes : entities.Where(entity => entity is Shape).Select(entity => (entity as Shape)!).ToArray(),
			specials: entities.Where(entity => entity is Special).Select(entity => (entity as Special)!).ToArray(),
			groups : entities.Where(entity => entity is Group).Select(entity => (entity as Group)!).ToArray(),
			joints : entities.Where(entity => entity is Joint).Select(entity => (entity as Joint)!).ToArray(),
			triggers : entities.Where(entity => entity is Trigger).Select(entity => (entity as Trigger)!).ToArray()) {}
	private Level(Info info=default!, 
		Shape[]? shapes = null,
		Special[]? specials = null,
		Group[]? groups = null,
		Joint[]? joints = null,
		Trigger[]? triggers = null) 
		: base("levelXML")
	{
		Info = info ?? new Info();
		if (shapes is not null && shapes.Length != 0) { Shapes = new(shapes); }
		if (specials is not null && specials.Length != 0) { Specials = new(specials); }
		if (groups is not null && groups.Length != 0) { Groups = new(groups); }
		if (joints is not null && joints.Length != 0) { Joints = new(joints); }
		if (triggers is not null && triggers.Length != 0) { Triggers = new(triggers); }
		elt = new("levelXML");
	}
	/// <summary>
	/// This constructor makes a Level from a valid levelXML string.
	/// If there are problems with your LevelXML, this constructor
	/// will throw an exception.
	/// </summary>
	public Level(string xml) : this(StrToXElement(xml)) {}
	internal Level(XElement e) : base("levelXML")
	{
		if (e.Name.ToString() != "levelXML")
		{
			throw new Exception("You didn't give me a LevelXML tag!");
		}
		XElement? InfoTag = e.Element("info");
		if (InfoTag is null) { 
			throw new Exception("Level is missing an info tag!"); 
		}
		Info = new(InfoTag);
		XElement? ShapesTag = e.Element("shapes");
		if (ShapesTag is not null) { Shapes = new DepthOneTag<Shape>(ShapesTag); }
		XElement? SpecialsTag = e.Element("specials");
		if (SpecialsTag is not null) { Specials = new DepthOneTag<Special>(SpecialsTag); }
		XElement? GroupsTag = e.Element("groups");
		if (GroupsTag is not null) { Groups = new DepthOneTag<Group>(GroupsTag); }
		XElement? JointsTag = e.Element("joints");
		if (JointsTag is not null) { Joints = new DepthOneTag<Joint>(JointsTag); }
		XElement? TriggersTag = e.Element("triggers");
		if (TriggersTag is not null) { Triggers = new DepthOneTag<Trigger>(TriggersTag, ReverseMapper: reverseMapper); }
		// At this point the depth one tags can be indexed through
		depthOneTagsReady.Set();
		if (Triggers is not null) { Triggers.finishConstruction(); }
	}
}
