namespace DSS_Scoring.DTOs
{
    public class AlternativaDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }

    public class AlternativaWithDetailsDTO
    {
        public int IdAlternativa { get; set; }
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<CriterioWithValorDTO> Criterios { get; set; }
    }
}
