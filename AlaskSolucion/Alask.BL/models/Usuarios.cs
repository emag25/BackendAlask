namespace Alask.BL
{
    public class Usuarios
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public string? password{ get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
        public Provincia? provincia { get; set; }
        public string? rol { get; set; }
        public DateOnly FechaIngreso { get; set; }

    }
}
