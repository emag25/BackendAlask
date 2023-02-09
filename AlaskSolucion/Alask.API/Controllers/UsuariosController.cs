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


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Usuarios>> GetUsuariosTodos(Usuarios usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("user_Getuser", cadenaConexion, "consultar_todo", xmlParam.ToString());

            List<Usuarios> listData = new List<Usuarios>();

            if (dsResultado.Tables.Count >= 0 || dsResultado.Tables[0].Rows.Count >=0)
            {
                string JSONstring = string.Empty;
                JSONstring = JsonConvert.SerializeObject(dsResultado.Tables[0]);
                return Ok(JSONstring);

            }else
            {
                return Ok();
            }


        }
    }
}

