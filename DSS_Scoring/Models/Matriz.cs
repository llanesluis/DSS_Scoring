namespace DSS_Scoring.Models;

public partial class Matriz
{
    public required int IdProyecto { get; set; }
    public required int IdAlternativa { get; set; }
    public required int IdCriterio { get; set; }
    public required int Valor { get; set; }

    public virtual Alternativa? IdAlternativaNavigation { get; set; } 
    public virtual Criterio? IdCriterioNavigation { get; set; } 
    public virtual Proyecto? IdProyectoNavigation { get; set; } 
}
