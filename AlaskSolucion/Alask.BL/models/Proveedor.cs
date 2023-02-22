
namespace Alask.BL
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string? Ruc { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public Provincia? Provincia { get; set; }
        public string? Logo { get; set;}
        public DateOnly FechaAprobacion { get; set; }
        public string? Estado { get; set; }
        public string? Transaccion { get; set; }

    }
}
