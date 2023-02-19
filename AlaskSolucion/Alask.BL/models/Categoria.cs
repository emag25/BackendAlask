namespace Alask.BL
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public DateOnly FechaIngreso { get; set; }

        public string? Estado { get; set; }

    }
}