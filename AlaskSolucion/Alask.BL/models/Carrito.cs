namespace Alask.BL
{
    public class Carrito
    {
        public int? Id { get; set; }
        public int? Usuario { get; set; }
        public float? Subtotal { get; set; }

        public float? Impuestos { get; set; }

        public float? Total { get; set; }

        public string? Estado { get; set; }
        
        public string? Transaccion { get; set; }



    }
}