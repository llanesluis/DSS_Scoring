namespace DSS_Scoring.Models;

public partial class Matriz
{
    public int IdProyecto { get; set; }

    public int IdAlternativa { get; set; }

    public int IdCriterio { get; set; }

    public int Valor { get; set; }

    public virtual Alternativa? IdAlternativaNavigation { get; set; } 

    public virtual Criterio? IdCriterioNavigation { get; set; } 

    public virtual Proyecto? IdProyectoNavigation { get; set; } 
}
