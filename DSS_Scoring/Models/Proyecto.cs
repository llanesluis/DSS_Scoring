using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Scoring.Models
{
    public class Proyecto
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Objetivo { get; set; }

        // Relaciones uno a muchos

        public ICollection<Criterio> Criterios { get; set; }

        public ICollection<Alternativa> Alternativas { get; set; }

        public ICollection<Puntuacion> Puntuaciones { get; set; }

        public ICollection<Resultado> Resultados { get; set; }
    }
}
