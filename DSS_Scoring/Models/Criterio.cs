namespace DSS_Scoring.Models;

public partial class Criterio
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Peso { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<Matriz> Matrices { get; set; } = new List<Matriz>();
}
