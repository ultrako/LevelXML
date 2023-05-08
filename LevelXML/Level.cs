using System.Xml.Linq;
namespace HappyWheels;

public class Level : LevelXMLTag
{
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
	// I expect to make meta-levels that don't actually stand by themselves,
	// but are meant to be combined into another level; in that case I'll have several
	// lists of Entities
	public Level(Info info=default!, params List<Entity>[] lists) : 
		this(info: info, entities: lists.SelectMany(lst => lst).ToArray()) {}
	// The most convenient way to construct a level is to give a info tag,
	// and a list of entities.
	public Level(Info info=default!, params Entity[] entities) :
		this(info : info,
			shapes : entities.Where(entity => entity is Shape).Select(entity => (entity as Shape)!).ToArray(),
			specials: entities.Where(entity => entity is Special).Select(entity => (entity as Special)!).ToArray(),
			groups : entities.Where(entity => entity is Group).Select(entity => (entity as Group)!).ToArray(),
			joints : entities.Where(entity => entity is Joint).Select(entity => (entity as Joint)!).ToArray(),
			triggers : entities.Where(entity => entity is Trigger).Select(entity => (entity as Trigger)!).ToArray()) {}
	public Level(Info info=default!, 
		Shape[]? shapes = null,
		Special[]? specials = null,
		Group[]? groups = null,
		Joint[]? joints = null,
		Trigger[]? triggers = null) 
		: base("levelXML")
	{
		Info = info ?? new Info();
		if (shapes is not null) { Shapes = new(shapes); }
		if (specials is not null) { Specials = new(specials); }
		if (groups is not null) { Groups = new(groups); }
		if (joints is not null) { Joints = new(joints); }
		if (triggers is not null) { Triggers = new(triggers); }
		elt = new("levelXML");
	}
}
