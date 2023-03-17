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
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetVentas, cadenaConexion,"consultar_todo", xmlParam.ToString());
            List<Venta> listData = new List<Venta>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Venta objResponse = new Venta {
                            Id = Convert.ToInt32(row["id_ventas"]),
                            Carrito = Convert.ToInt32(row["carritos_id_ventas"]),
                            Usuario = row["usuario_id_ventas"].ToString(),
                            Email = row["email_usuario"].ToString(),
                            Telefono = row["telefono_usuario"].ToString(),
                            Provincia = row["nombre_provincia"].ToString(),
                            Direccion = row["direccion_usuario"].ToString(),
                            Total = float.Parse(row["total_carrito"].ToString()),
                            Estado = row["estado_ventas"].ToString()
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