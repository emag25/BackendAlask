namespace Alask.BL
{
    public class Venta
    {
        public int? Id { get; set; }
        public int? Usuario { get; set; }

        public int? Carrito { get; set; }

        public int? Tarjeta { get; set; }
        
        public string? Estado { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Transaccion { get; set; }



    }
}