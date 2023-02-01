using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API {

    [ApiController]
    [Route("api/[controller]")]

    public class SolicitudesController : Controller {


        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<Solicitud>> GetAll()
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(new Solicitud());
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetSolicitudes", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Solicitud> listData = new List<Solicitud>();

            return Ok(listData);
        }


    }

}