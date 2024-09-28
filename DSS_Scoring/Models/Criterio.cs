using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS_Scoring.Models
{
    public class Criterio
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey("Proyecto")] public int IdProyecto { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int Peso { get; set; }
   
        public Proyecto Proyecto { get; set; } // Relación muchos a uno con Proyecto
        public ICollection<Puntuacion> Puntuaciones { get; set; } // Relación uno a muchos con Puntuaciones
    }
}
