using DSS_Scoring.Models;

namespace DSS_Scoring.DTOs
{
    public class ResultadoDTO
    {
        public int IdProyecto { get; set; }
        public int IdAlternativa { get; set; }
        public required int Score { get; set; }
    }    
    
    public class ResultadoWithDetailsDTO
    {
        public int IdProyecto { get; set; }
        public int IdAlternativa { get; set; }
        public required int Score { get; set; }

        public required virtual ProyectoDTO Proyecto { get; set; }
        public required virtual AlternativaDTO Alternativa { get; set; }
    }
}
