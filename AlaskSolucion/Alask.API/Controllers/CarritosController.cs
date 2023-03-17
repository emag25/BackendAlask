using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Xml.Linq;


namespace Alask.API
{
    [ApiController]
    [Route("api/[controller]")]

    public class CarritosController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Carrito>> Get([FromBody] Carrito p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetCarritos, cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Carrito> listData = new List<Carrito>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Carrito objResponse = new Carrito {                      
                            Id = Convert.ToInt32(row["id_carrito"]),
                            Id_item = Convert.ToInt32(row["id_item"]),                                                            
                            Nombre = row["nombre_producto"].ToString(),
                            Cantidad = Convert.ToInt32(row["cantidad_item_carrito"]),
                            Total = float.Parse(row["total_item_carrito"].ToString())
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