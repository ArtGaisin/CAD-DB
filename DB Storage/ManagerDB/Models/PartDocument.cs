namespace ManagerDB.Models;

public class PartDocument
   (string fullName,
    Developer autor,
    Material material,
    Guid? id = null,
    DrawingDocument? drawingDocument = null) : Document(fullName, autor, id), IEntity
{   
    public Material Material { get; } = material;
    public DrawingDocument? Drawing { get; } = drawingDocument;
}
