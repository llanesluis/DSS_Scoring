using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Scoring.Models
{
    public class Puntuacion
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey("Proyecto")] public int IdProyecto { get; set; }
        [ForeignKey("Alternativa")] public int IdAlternativa { get; set; }
        [ForeignKey("Criterio")] public int IdCriterio { get; set; }
        public required int Valor { get; set; }

        public Proyecto Proyecto { get; set; }
        public Alternativa Alternativa { get; set; }
        public Criterio Criterio { get; set; }
    }
}
