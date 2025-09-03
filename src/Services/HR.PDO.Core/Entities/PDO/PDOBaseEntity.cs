namespace HR.PDO.Core.Entities.PDO;

/// <summary>
/// Base entity for all domain entities
/// </summary>
public abstract class PDOBaseEntity
{
    public int Id { get; set; }
    
    public bool? StatusAktif { get; set; }
    public DateTime? TarikhCipta { get; set; }
    public Guid IdCipta { get; set; }
    public DateTime? TarikhPinda{ get; set; }
    public Guid? IdPinda { get; set; }
    public Guid? IdHapus { get; set; }
    public DateTime? TarikhHapus { get; set; }
}
