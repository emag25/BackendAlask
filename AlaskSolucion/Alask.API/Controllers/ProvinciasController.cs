using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API {

    [ApiController]
    [Route("api/[controller]")]

    public class ProvinciasController : Controller {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Provincia>> Get([FromBody] Provincia p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetProvincias, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Provincia> listData = new List<Provincia>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Provincia objResponse = new Provincia
                        {
                            Id = Convert.ToInt32(row["id_provincia"]),
                            Nombre = row["nombre_provincia"].ToString()
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

    }

}