namespace ManagerDB.Models;

public class AssemblyDocument
    (string fullName,
    Developer autor,
    Guid? id = null,
    DrawingDocument? drawingDocument = null) : Document(fullName, autor, id), IEntity
{
    public DrawingDocument? DrawingDocument { get; } = drawingDocument;
    public List<PartDocument> Parts { get; } = [];
}
