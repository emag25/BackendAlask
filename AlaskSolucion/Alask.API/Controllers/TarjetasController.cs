using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;


namespace Alask.API
{

    [Route("api/[controller]")]
    [ApiController]

    public class TarjetasController : Controller
    {


        [Route("[action]/{nombreprocedimiento}")]
        [HttpPost]
        public async Task<ActionResult<Tarjeta>> GetTarjeta(Tarjeta tarjetas, string nombreprocedimiento)
        {
            Console.WriteLine(nombreprocedimiento);
            if (nombreprocedimiento == "todo")
            {
                var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
                XDocument xmlParam = DBXmlMethods.GetXml(tarjetas);
                DataSet dsResultado = await DBXmlMethods.EjecutaBase("Tarjeta_Gettarjeta", cadenaConexion, "consultar_todo", xmlParam.ToString());

                List<Tarjeta> listData = new List<Tarjeta>();

                if (dsResultado.Tables.Count >= 0 || dsResultado.Tables[0].Rows.Count >= 0)
                {
                    string JSONstring = string.Empty;
                    JSONstring = JsonConvert.SerializeObject(dsResultado.Tables[0]);
                    return Ok(JSONstring);

                }
                else
                {
                    return Ok();
                }
            }
            
           
            else
            {
                return BadRequest();

            }




       
        
       }



        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Response>> Set([FromBody] Tarjeta t)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(t);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetTarjetas, cadenaConexion, t.Transaccion, xmlParam.ToString());
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






