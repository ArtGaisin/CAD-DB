namespace ManagerDB.Models;

public class Developer
    (string name,
    Guid? id = null) : IEntity
{
    public string Name { get; } = name;
    public Guid Id { get; } = id ?? Guid.NewGuid();
    public List<PartDocument> PartDocuments { get; } = [];
    public List<DrawingDocument> DrawingDocuments { get; } = [];
}
