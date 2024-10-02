namespace DSS_Scoring.DTOs
{
    public class CriterioDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Peso { get; set; }
    }

    public class CriterioWithValorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public int Valor { get; set; }
    }
}
