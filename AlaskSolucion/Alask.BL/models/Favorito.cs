namespace Alask.BL
{
    public class Favorito
    {
        public int? Id { get; set; }
        public Usuario? Usuario { get; set; }
        public Producto? Producto { get; set; }

        public DateOnly? FechaIngreso { get; set; }

        public string? Estado { get; set; }
        public string? Transaccion { get; set; }



    }
}