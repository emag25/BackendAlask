using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API
{

    [Route("api/[controller]")]
    [ApiController]

    public class CategoriasController : Controller
    {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Categoria>> GetCategoriasTodos([FromBody] Categoria categorias)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(categorias);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("Categorias_GetCategorias", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Categoria> listData = new List<Categoria>();

            return Ok(listData);
        }


    }

}
