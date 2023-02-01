using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API {

	[Route("api/[controller]")]
	[ApiController]

	public class ProveedoresController : Controller {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> GetProveedoresTodos([FromBody] Proveedor proveedores)
		{
			var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
			XDocument xmlParam = DBXmlMethods.GetXml(proveedores);
			DataSet dsResultado = await DBXmlMethods.EjecutaBase("Proveedores_GetProveedores", cadenaConexion, "consultar_todo", xmlParam.ToString());
			List<Proveedor> listData = new List<Proveedor>();

            return Ok(listData);
        }


    }

}