namespace ManagerDB.Models;
public class Material
    (string name,
    Guid? id = null) : IEntity
{
    public Guid Id { get; } = id ?? Guid.NewGuid();
    public string Name { get; } = name;
}
