using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Scoring.Models
{
    public class Resultado
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey("Proyecto")] public int IdProyecto { get; set; }
        [ForeignKey("Alternativa")] public int IdAlternativa { get; set; }
        public required int PuntuacionTotal { get; set; }
       
        public Proyecto Proyecto { get; set; } // Relación muchos a uno con Proyecto
        public Alternativa Alternativa { get; set; } // Relación muchos a uno con Alternativa
    }
}
