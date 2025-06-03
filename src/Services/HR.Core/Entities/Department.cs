namespace HR.Core.Entities;

/// <summary>
/// Department entity representing an organizational unit
/// </summary>
public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    // Navigation properties would be handled in the Infrastructure layer
}
