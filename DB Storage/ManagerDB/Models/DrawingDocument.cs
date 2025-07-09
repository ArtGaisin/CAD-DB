namespace ManagerDB.Models;

public class DrawingDocument
    (string fullName,
    Developer autor,
    Guid? id = null,
    PartDocument? partDocument = null) : Document(fullName, autor, id), IEntity
{   
    public PartDocument? Part { get; } = partDocument;
}
