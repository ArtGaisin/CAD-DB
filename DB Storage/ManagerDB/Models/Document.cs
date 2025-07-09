namespace ManagerDB.Models;

public abstract class Document
    (string fullName,
    Developer autor,
    Guid? id = null) : IEntity
{
    public string FullName { get; } = fullName;
    public Guid Id { get; } = id ?? Guid.NewGuid();
    public Developer Autor { get; } = autor;
}
