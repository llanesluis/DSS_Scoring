namespace DSS_Scoring.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Objetivo { get; set; } = null!;

    public virtual ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();

    public virtual ICollection<Criterio> Criterios { get; set; } = new List<Criterio>();

    public virtual ICollection<Matriz> Matrices { get; set; } = new List<Matriz>();

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
