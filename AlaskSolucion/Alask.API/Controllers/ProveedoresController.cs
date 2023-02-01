using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API {

    [ApiController]
    [Route("api/[controller]")]

    public class ProveedoresController : Controller {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> GetAll([FromBody] Proveedor proveedor)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(proveedor);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProveedores", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Proveedor> listData = new List<Proveedor>();

            return Ok(listData);
        }



        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> GetById([FromBody] Proveedor proveedor)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(proveedor);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProveedores", cadenaConexion, "consultar_porId", xmlParam.ToString());
            List<Proveedor> listData = new List<Proveedor>();

            return Ok(listData);
        }



        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> GetByName([FromBody] Proveedor proveedor)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(proveedor);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProveedores", cadenaConexion, "consultar_porNombre", xmlParam.ToString());
            List<Proveedor> listData = new List<Proveedor>();

            return Ok(listData);
        }


    }

}