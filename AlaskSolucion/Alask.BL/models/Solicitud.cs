
namespace Alask.BL
{
    public class Solicitud
    {
        public int? Id { get; set; }
        public string? Ruc { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public Provincia? Provincia { get; set; }
        public DateOnly? FechaEnvio { get; set; }
        public string? Estado { get; set; }

    }
}
