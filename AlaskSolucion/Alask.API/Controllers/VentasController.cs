using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Xml.Linq;


namespace Alask.API
{
    [ApiController]
    [Route("api/[controller]")]

    public class VentasController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Venta>> Get([FromBody] Venta p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetVentas, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Venta> listData = new List<Venta>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Venta objResponse = new Venta {                      
                            Id = Convert.ToInt32(row["id_ventas"]),
                            Usuario = Convert.ToInt32(row["usuario_id_ventas"]),
                            Carrito = Convert.ToInt32(row["carritos_id_ventas"]),
                            Tarjeta = Convert.ToInt32(row["tarjeta_id_ventas"]),
                            Estado = row["estado_ventas"].ToString(),
                            Fecha = DateOnly.FromDateTime(DateTime.Parse(row["fecha_ventas"].ToString()))
                        };
                        listData.Add(objResponse);

                    }

                 }
                catch (Exception ex)
                {
                    Console.WriteLine("---- ERROR ---- " + ex.Message);
                }
            }

            return Ok(listData);
        }


    }
    

}