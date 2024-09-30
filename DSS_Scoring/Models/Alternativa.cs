namespace DSS_Scoring.Models;

public partial class Alternativa
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }


    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<Matriz> Matrices { get; set; } = new List<Matriz>();

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
