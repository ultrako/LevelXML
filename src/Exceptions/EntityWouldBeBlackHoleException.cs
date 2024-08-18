using LevelXML;

/// <summary>
/// This exception represents the fact that the faulty entity would
/// be a black hole when the level loads.
/// </summary>
class EntityWouldBeBlackHoleException : LevelXMLException
{
    public Entity FaultyEntity {get; private set;}
    public EntityWouldBeBlackHoleException(string message, Entity faultyEntity) : base(message)
    {
        FaultyEntity = faultyEntity;
    }
}