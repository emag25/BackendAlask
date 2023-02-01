namespace Alask.BL
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Password{ get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public Provincia? Provincia { get; set; }
        public string? Rol { get; set; }
        public DateOnly FechaIngreso { get; set; }

    }
}
