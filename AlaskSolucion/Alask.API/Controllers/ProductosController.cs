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

    public class ProductosController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Producto>> Get([FromBody] Producto p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetProductos, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Producto> listData = new List<Producto>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {                        
                        Producto objResponse = new Producto
                        {
                            Id = Convert.ToInt32(row["id_producto"]),
                            Nombre = row["nombre_producto"].ToString(),
                            Descripcion = row["descripcion_producto"].ToString(),
                            Imagen = row["imagen_producto"].ToString(),
                            Precio = float.Parse(row["precio_producto"].ToString()),
                            Stock = int.Parse(row["stock_producto"].ToString()),
                            Categoria = Convert.ToInt32(row["categoria_producto"]),                            
                            Proveedor = Convert.ToInt32(row["proveedor_producto"]),
                            FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_producto"].ToString())),
                            Estado = row["estado_producto"].ToString()
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Response>> Set([FromBody] Producto p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetProductos, cadenaConexion, p.Transaccion, xmlParam.ToString());
            Response objResponse = new Response();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                }
                catch (Exception e)
                {
                    objResponse.Respuesta = "---- ERROR ---- ";
                }

            }
            return Ok(objResponse);
        }

    }

}
