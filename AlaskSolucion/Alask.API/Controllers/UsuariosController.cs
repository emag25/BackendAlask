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

    public class UsuariosController : Controller
    {


        [Route("[action]/{nombreprocedimiento}")]
        [HttpPost]
        public async Task<ActionResult<Usuarios>> GetUsuarios(Usuarios usuarios, string nombreprocedimiento)
        {
            Console.WriteLine(nombreprocedimiento);
            if (nombreprocedimiento == "todo")
            {
                var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
                XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
                DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "consultar_todo", xmlParam.ToString());

                List<Usuarios> listData = new List<Usuarios>();

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
            else if (nombreprocedimiento == "apellido")
            {
                var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
                XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
                DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "consultar_porApellido", xmlParam.ToString());

                List<Usuarios> listData = new List<Usuarios>();

                if (dsResultado.Tables[0].Rows.Count > 0)
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
            else if (nombreprocedimiento == "email")
            {
                var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
                XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
                DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "consultar_porEmail", xmlParam.ToString());

                List<Usuarios> listData = new List<Usuarios>();

                if (dsResultado.Tables[0].Rows.Count > 0)
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
            else if (nombreprocedimiento == "Verificar")
            {
                var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
                XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
                DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "Verificar", xmlParam.ToString());

                List<Usuarios> listData = new List<Usuarios>();

                if (dsResultado.Tables[0].Rows.Count > 0)
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
        public async Task<ActionResult<Response>> Set([FromBody] Usuarios u)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(u);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetUsuarios, cadenaConexion,u.Transaccion, xmlParam.ToString());
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




    

