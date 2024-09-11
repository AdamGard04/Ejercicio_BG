namespace WebAPP_Adam_Garcia_2024_09_10.DTOs
{
    public class AsientoContableDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int DepartamentoId { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
