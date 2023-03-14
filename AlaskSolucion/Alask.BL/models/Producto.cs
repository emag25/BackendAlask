namespace Alask.BL
{
    public class Producto
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public float? Precio { get; set; }
        public int? Stock { get; set; }
        public int Categoria { get; set; }
        public int Proveedor { get; set; }
        public DateOnly? FechaIngreso { get; set; }
        public string? Estado { get; set; }        
    }
}