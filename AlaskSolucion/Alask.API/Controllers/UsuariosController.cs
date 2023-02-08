using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : Controller
    {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Usuarios>> GetUsuariosTodos([FromBody] Usuarios Usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(Usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Usuarios> listData = new List<Usuarios>();

            return Ok(listData);
        }


    }
}

