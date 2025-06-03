namespace HR.Core.Entities;

/// <summary>
/// Position entity representing a job role within a department
/// </summary>
public class Position : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid DepartmentId { get; set; }
    
    // Navigation properties would be handled in the Infrastructure layer
}
