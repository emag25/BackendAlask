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

    public class CategoriasController : Controller
    {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Categoria>> Get([FromBody] Categoria p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetCategorias, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Categoria> listData = new List<Categoria>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {

                        Categoria objResponse = new Categoria
                        {
                            Id = Convert.ToInt32(row["id_categoria"]),
                            Nombre = row["nombre_categoria"].ToString(),
                            FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_categoria"].ToString())),
                            Estado = row["estado_categoria"].ToString()
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
        public async Task<ActionResult<Response>> Set([FromBody] Categoria p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetCategorias, cadenaConexion, p.Transaccion, xmlParam.ToString());
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
