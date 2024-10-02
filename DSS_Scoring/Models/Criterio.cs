namespace DSS_Scoring.Models;

public partial class Criterio
{
    public int Id { get; set; }
    public int IdProyecto { get; set; }
    public required string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public required int Peso { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
    public virtual ICollection<Matriz> Matrices { get; set; } = new List<Matriz>();
}
