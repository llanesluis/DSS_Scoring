namespace DSS_Scoring.Shared.DTOs

{
    public class MatrizDTO
    {
        public int IdProyecto { get; set; }
        public int IdAlternativa { get; set; }
        public int IdCriterio { get; set; }
        public int Valor { get; set; }
    }

    public class MatrizWithDetailsDTO
    {
        public int IdProyecto { get; set; }
        public int IdAlternativa { get; set; }
        public int IdCriterio { get; set; }
        public int Valor { get; set; }
        public virtual AlternativaDTO? Alternativa { get; set; }
        public virtual CriterioDTO? Criterio { get; set; }
    }
}
