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

    public class FavoritosController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Favorito>> Get([FromBody] Favorito p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetFavoritos, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Favorito> listData = new List<Favorito>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Favorito objResponse = new Favorito {                      
                            Id = Convert.ToInt32(row["id_favoritos"]),
                            Usuario = Convert.ToInt32(row["id_usuario"]),                                                            
                            Producto = Convert.ToInt32(row["id_producto"]),                                                            
                            FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_favorito"].ToString())),
                            Estado = row["estado_producto_favorito"].ToString()
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
        public async Task<ActionResult<Response>> Set([FromBody] Favorito p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetFavoritos, cadenaConexion, p.Transaccion, xmlParam.ToString());
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