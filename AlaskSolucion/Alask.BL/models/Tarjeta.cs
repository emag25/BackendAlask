﻿
namespace Alask.BL
{

    public class Tarjeta
    {
        public int id { get; set; }
        public string? tipo { get; set; }
        public string? numero { get; set; }
        public int? mes { get; set; }
        public int? year { get; set; }
        public int? cvv { get; set; }

        public Usuarios? usuario { get; set; }









    }
}
