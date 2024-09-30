namespace DSS_Scoring.Models;

public partial class Resultado
{
    public int IdProyecto { get; set; }

    public int IdAlternativa { get; set; }

    public int? Score { get; set; }

    public virtual Alternativa? IdAlternativaNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; } 
}
