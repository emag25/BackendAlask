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
        public async Task<ActionResult<Usuarios>> GetUsuariosTodos([FromBody] Usuario usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(proveedores);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("Usuarios_GetUsuarios", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Usuario> listData = new List<Usuario>();

            return Ok(listData);
        }


    }

