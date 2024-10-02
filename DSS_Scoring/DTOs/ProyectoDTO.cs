using DSS_Scoring.Models;

namespace DSS_Scoring.DTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Objetivo { get; set; } = null!;
    }

    public class ProyectoWithDetailsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Objetivo { get; set; } = null!;
        public virtual ICollection<AlternativaDTO> Alternativas { get; set; } = new List<AlternativaDTO>();
        public virtual ICollection<CriterioDTO> Criterios { get; set; } = new List<CriterioDTO>();
    }
}
