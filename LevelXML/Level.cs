using System.Linq;
using System.Xml.Linq;
using System.Collections;
namespace HappyWheels;

/// <summary>
/// The Level class represents a Happy Wheels level.
/// </summary>
/// <remarks>
/// As this extends LevelXMLTag,
/// ToXML() will print out the LevelXML that represents this level,
/// ready to be pasted into the happy wheels import box.
/// </remarks>
public class Level : LevelXMLTag
{
	public IList<Shape> Shapes { get { return ShapesTag; } }
	public IList<Special> Specials { get { return SpecialsTag; } }
	public IList<Group> Groups { get { return GroupsTag; } }
	public IList<Joint> Joints { get { return JointsTag; } }
	public IList<Trigger> Triggers { get { return TriggersTag; } }
	// These are never null but you can't use ! on a struct data type
	public double X { get {return info.X ?? 0;} set { info.X = value;} }
	public double Y { get {return info.Y ?? 0;} set { info.Y = value;} }
	public double Character { get { return info.Character ?? 0; } set { info.Character = value;} }
	public HWBool ForcedCharacter { get { return info.ForcedCharacter ?? false; } set { info.ForcedCharacter = value;}}
	public HWBool VehicleHidden { get { return info.VehicleHidden ?? false; } set { info.VehicleHidden = value;}}
	public double Background { get { return info.Background ?? 0; } set { info.Background = value;} }
	public double BackgroundColor { get { return info.BackgroundColor ?? 0; } set { info.BackgroundColor = value;} }
	private Info info;
	public IEnumerable<Entity> Entities =>
		(Shapes as IEnumerable<Entity>)
		.Concat(Specials)
		.Concat(Groups)
		.Concat(Joints)
		.Concat(Triggers);
	/// <summary>
	/// This constructor makes a Level from a valid levelXML string.
	/// </summary>
	public Level(string xml) : this(StrToXElement(xml)) {}
	/// <summary>
	/// This constructor makes a Level from an Info tag and several Entities 
	/// (which are shapes, specials, groups, joints, and triggers).
	/// If no info tag is supplied, then the default one from opening the level
	/// editor and just hitting save is set.
	/// </summary>
	public Level(params Entity[] entities) :
		this(info : null,
			shapes : entities.Where(entity => entity is Shape).Select(entity => (entity as Shape)!).ToArray(),
			specials: entities.Where(entity => entity is Special).Select(entity => (entity as Special)!).ToArray(),
			groups : entities.Where(entity => entity is Group).Select(entity => (entity as Group)!).ToArray(),
			joints : entities.Where(entity => entity is Joint).Select(entity => (entity as Joint)!).ToArray(),
			triggers : entities.Where(entity => entity is Trigger).Select(entity => (entity as Trigger)!).ToArray()) {}
	private DepthOneTag<Shape> ShapesTag;
	private DepthOneTag<Special> SpecialsTag;
	private DepthOneTag<Group> GroupsTag;
	private DepthOneTag<Joint> JointsTag;
	private DepthOneTag<Trigger> TriggersTag;
	internal override void PlaceInLevel(Func<Entity, int> _)
	{
		elt.RemoveNodes();
		elt.Add(info.elt);
		foreach (DepthOneTag tag in new List<DepthOneTag> {ShapesTag, SpecialsTag, GroupsTag, JointsTag, TriggersTag})
		{	
			if (tag.Count > 0)
			{
				elt.Add(tag.elt);
			}
		}
		JointsTag.PlaceInLevel(mapper);
		TriggersTag.PlaceInLevel(mapper);
		SpecialsTag.PlaceInLevel(mapper);
		ShapesTag.PlaceInLevel(vertMapper);
		GroupsTag.PlaceInLevel(vertMapper);
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
			nameof(Shape) => ShapesTag,
			nameof(Special) => SpecialsTag,
			nameof(Group) => GroupsTag,
			nameof(Joint) => JointsTag,
			nameof(Trigger) => TriggersTag,
			_ => throw new Exception($"Levels don't hold the type {t.Name}!"),
		};
	}
	private int vertMapper(Entity e)
	{
		return e switch
		{
			Art a => 
				Shapes
				.Where(shape => shape is Art)
				.Concat(Groups.SelectMany(group => group)
				.Where(entity => entity is Art))
				.ToList()
				.FindIndex(other => other == a),
			Polygon p => 
				Shapes
				.Where(shape => shape is Polygon)
				.Concat(Groups.SelectMany(group => group)
				.Where(entity => entity is Polygon))
				.ToList()
				.FindIndex(other => other == p),
			_ => throw new Exception("Art shape pointed to by another art shape was not found!"),
		};
	}
	private int mapper(Entity e)
	{
		int index = e switch
		{
			Shape s => (ShapesTag ?? new DepthOneTag<Shape>()).IndexOf(s),
			Special s => (SpecialsTag ?? new DepthOneTag<Special>()).IndexOf(s),
			Group s => (GroupsTag ?? new DepthOneTag<Group>()).IndexOf(s),
			Joint s => (JointsTag ?? new DepthOneTag<Joint>()).IndexOf(s),
			Trigger s => (TriggersTag ?? new DepthOneTag<Trigger>()).IndexOf(s),
			_ => -1,
		};
		if (index < 0)
		{
			throw new Exception($"Entity {e.GetHashCode()} pointed to something that wasn't in the level!");
		}
		return index;
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
	private Level(Info? info=default!, 
		Shape[]? shapes = null,
		Special[]? specials = null,
		Group[]? groups = null,
		Joint[]? joints = null,
		Trigger[]? triggers = null) 
		: base("levelXML")
	{
		this.info = info ?? new Info();
 		ShapesTag = new(shapes);
		SpecialsTag = new(specials);
		GroupsTag = new(groups);
		JointsTag = new(joints);
		TriggersTag = new(triggers);
		elt = new("levelXML");
	}
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
		info = new(InfoTag);
		XElement? ShapesElement = e.Element("shapes");
		ShapesTag = new DepthOneTag<Shape>(ShapesElement);
		XElement? SpecialsElement = e.Element("specials");
		SpecialsTag = new DepthOneTag<Special>(SpecialsElement);
		XElement? GroupsElement = e.Element("groups");
		GroupsTag = new DepthOneTag<Group>(GroupsElement);
		XElement? JointsElement = e.Element("joints");
		JointsTag = new DepthOneTag<Joint>(JointsElement);
		XElement? TriggersElement = e.Element("triggers");
		TriggersTag = new DepthOneTag<Trigger>(TriggersElement, ReverseMapper: reverseMapper);
		// At this point the depth one tags can be indexed through
		depthOneTagsReady.Set();
		TriggersTag.finishConstruction();
	}
}
