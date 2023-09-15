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
public class Level : LevelXMLTag, IConvertableToXML
{
	public IList<Shape> Shapes { get { return shapesTag.lst; } }
	public IList<Special> Specials { get { return specialsTag.lst; } }
	public IList<Group> Groups { get { return groupsTag.lst; } }
	public IList<Joint> Joints { get { return jointsTag.lst; } }
	public IList<Trigger> Triggers { get { return triggersTag.lst; } }
	// These are never null but you can't use ! on a struct data type
	public double X { get {return info.X ?? 0;} set { info.X = value;} }
	public double Y { get {return info.Y ?? 0;} set { info.Y = value;} }
	public Character? Character { get { return info.Character; } set { info.Character = value ?? (Character)double.NaN;} }
	public HWBool ForcedCharacter { get { return info.ForcedCharacter ?? false; } set { info.ForcedCharacter = value;}}
	public HWBool VehicleHidden { get { return info.VehicleHidden ?? false; } set { info.VehicleHidden = value;}}
	public double Background { get { return info.Background ?? 0; } set { info.Background = value;} }
	public double BackgroundColor { get { return info.BackgroundColor ?? 0; } set { info.BackgroundColor = value;} }
	public string ToXML() { return ToXML(mapper: default!); }
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
		this(info : new(),
			shapes : entities.Where(entity => entity is Shape).Select(entity => (entity as Shape)!).ToArray(),
			specials: entities.Where(entity => entity is Special).Select(entity => (entity as Special)!).ToArray(),
			groups : entities.Where(entity => entity is Group).Select(entity => (entity as Group)!).ToArray(),
			joints : entities.Where(entity => entity is Joint).Select(entity => (entity as Joint)!).ToArray(),
			triggers : entities.Where(entity => entity is Trigger).Select(entity => (entity as Trigger)!).ToArray()) {}
			
	private Info info;
	private DepthOneTag<Shape> shapesTag;
	private DepthOneTag<Special> specialsTag;
	private DepthOneTag<Group> groupsTag;
	private DepthOneTag<Joint> jointsTag;
	private DepthOneTag<Trigger> triggersTag;
	internal override void PlaceInLevel(Func<Entity, int> _)
	{
		Elt.RemoveNodes();
		Elt.Add(info.Elt);
		foreach (DepthOneTag tag in new List<DepthOneTag> {shapesTag, specialsTag, groupsTag, jointsTag, triggersTag})
		{	
			if (tag.Count > 0)
			{
				Elt.Add(tag.Elt);
			}
		}
		jointsTag.PlaceInLevel(Mapper);
		triggersTag.PlaceInLevel(Mapper);
		specialsTag.PlaceInLevel(Mapper);
		shapesTag.PlaceInLevel(VertMapper);
		groupsTag.PlaceInLevel(VertMapper);
	}
	private static readonly Dictionary<string, Type> nameToEntityType = new()
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
			nameof(Shape) => shapesTag,
			nameof(Special) => specialsTag,
			nameof(Group) => groupsTag,
			nameof(Joint) => jointsTag,
			nameof(Trigger) => triggersTag,
			_ => throw new LevelXMLException($"Levels don't hold the type {t.Name}!"),
		};
	}
	private int VertMapper(Entity e)
	{
		// If the vertex tag in this polygon or art has its own coordinates,
		// then we need to give it a unique id
		// Otherwise we need to find the first matching kind of art or poly tag
		// that has the same original id.
		if (e is Art art)
		{
			List<Art> matchingArts = Shapes.Where(shape => shape is Art)
				.Concat(Groups.SelectMany(group => group.Items)
				.Where(entity => entity is Art))
				.Select(entity => (Art)entity)
				.ToList();
			if (art.isEmpty)
			{
				return matchingArts.FindIndex(other => other.originalIndex == art.originalIndex);
			}
			else
			{
				return matchingArts.FindIndex(other => other == art);
			}
		} 
		else if (e is Polygon poly)
		{
			List<Polygon> matchingPolys = Shapes.Where(shape => shape is Polygon)
				.Concat(Groups.SelectMany(group => group.Items)
				.Where(entity => entity is Polygon))
				.Select(entity => (Polygon)entity)
				.ToList();
			if (poly.isEmpty)
			{
				return matchingPolys.FindIndex(other => other.originalIndex == poly.originalIndex);
			}
			else
			{
				return matchingPolys.FindIndex(other => other == poly);
			}
		}
		else
		{
			throw new Exception("Gave neither an art entity nor a polygon entity to the vertex id mapper!");
		}
		throw new Exception("Art shape pointed to by another art shape was not found!");
		
	}
	private int Mapper(Entity e)
	{
		int index = e switch
		{
			Shape s => (shapesTag ?? new DepthOneTag<Shape>()).lst.IndexOf(s),
			Special s => (specialsTag ?? new DepthOneTag<Special>()).lst.IndexOf(s),
			Group s => (groupsTag ?? new DepthOneTag<Group>()).lst.IndexOf(s),
			Joint s => (jointsTag ?? new DepthOneTag<Joint>()).lst.IndexOf(s),
			Trigger s => (triggersTag ?? new DepthOneTag<Trigger>()).lst.IndexOf(s),
			_ => -1,
		};
		if (index < 0)
		{
			throw new Exception($"Entity {e.GetHashCode()} pointed to something that wasn't in the level!");
		}
		return index;
	}
	private AutoResetEvent depthOneTagsReady = new(false);
	private Entity ReverseTargetMapper(XElement e)
	{
		int index = Int32.Parse(e.Attribute("i")!.Value)!;
		Type entityType = nameToEntityType[e.Name.ToString()];
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
			return lst.GetEntityAt(index);
		}
	}
	private Entity? ReverseJointMapper(string? entityIndex)
	{
		// This is the only valid way to represent being jointed to nothing
		if (entityIndex is null || entityIndex.Equals("-1")) { return null; }
		string indexTail = entityIndex[..^1];
		int index;
		if (int.TryParse(entityIndex, out index))
		{
			return Shapes[index];
		}
		else if (int.TryParse(indexTail, out index))
		{
			
			return entityIndex[0] switch
			{
				's' => Specials[index],
				'g' => Groups[index],
				_ => throw new Exception("Invalid joint index!")
			};
		} else {
			throw new Exception("Invalid joint index!");
		}
	}

	private Level(Info info, 
		Shape[] shapes,
		Special[] specials,
		Group[] groups,
		Joint[] joints,
		Trigger[] triggers) 
		: base("levelXML")
	{
		this.info = info ?? new Info();
 		shapesTag = new(shapes);
		specialsTag = new(specials);
		groupsTag = new(groups);
		jointsTag = new(joints);
		triggersTag = new(triggers);
		Elt = new("levelXML");
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
		shapesTag = new DepthOneTag<Shape>(ShapesElement);
		XElement? SpecialsElement = e.Element("specials");
		specialsTag = new DepthOneTag<Special>(SpecialsElement);
		XElement? GroupsElement = e.Element("groups");
		groupsTag = new DepthOneTag<Group>(GroupsElement);
		XElement? JointsElement = e.Element("joints");
		jointsTag = new DepthOneTag<Joint>(JointsElement, ReverseJointMapper: ReverseJointMapper);
		XElement? TriggersElement = e.Element("triggers");
		triggersTag = new DepthOneTag<Trigger>(TriggersElement, ReverseTargetMapper: ReverseTargetMapper);
		// At this point the depth one tags can be indexed through
		depthOneTagsReady.Set();
		triggersTag.FinishConstruction();
	}
}
