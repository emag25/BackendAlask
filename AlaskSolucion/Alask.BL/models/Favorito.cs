namespace Alask.BL
{
    public class Favorito
    {
        public int? Id { get; set; }
        public int? Usuario { get; set; }
        public int? Producto { get; set; }

        public DateOnly? FechaIngreso { get; set; }

        public string? Estado { get; set; }
        public string? Transaccion { get; set; }



    }
}