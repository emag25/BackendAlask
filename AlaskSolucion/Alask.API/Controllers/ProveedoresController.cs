using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProveedoresController : Controller {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> Get([FromBody] Proveedor p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetProveedores, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Proveedor> listData = new List<Proveedor>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach(DataRow row in dsResultado.Tables[0].Rows) 
                    {
                        Proveedor objResponse = new Proveedor
                        {
                            Id = Convert.ToInt32(row["id_proveedor"]),
                            Ruc = row["ruc_proveedor"].ToString(),
                            Nombre = row["nombre_proveedor"].ToString(),
                            Email = row["email_proveedor"].ToString(),
                            Telefono = row["telefono_proveedor"].ToString(),
                            Provincia = new Provincia()
                            {
                                Id = Convert.ToInt32(row["id_provincia"]),
                                Nombre = row["nombre_provincia"].ToString()
                            },
                            Logo = row["logo_proveedor"].ToString(),
                            FechaAprobacion = DateOnly.FromDateTime(DateTime.Parse(row["fechaAprobacion_proveedor"].ToString())),
                            Estado = row["estado_proveedor"].ToString()
                        };
                        listData.Add(objResponse);
                    }

                }catch(Exception ex)
                {
                    Console.WriteLine("---- ERROR ---- "+ex.Message);
                }
            }

            return Ok(listData);
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Response>> Set([FromBody] Proveedor p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetProveedores, cadenaConexion, p.Transaccion, xmlParam.ToString());
            
            Response objResponse = new Response();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["MENSAJE"].ToString();
                }
                catch (Exception e)
                {
                    objResponse.Respuesta = "---- ERROR ---- ";
                }

            }
            return Ok(objResponse);
        }



        [Route("[action]")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Proveedor s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetProveedores, cadenaConexion, s.Transaccion, xmlParam.ToString());

            Response objResponse = new Response();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["MENSAJE"].ToString();

                }
                catch (Exception e)
                {
                    objResponse.Respuesta = "---- ERROR ---- ";
                }
            }
            return Ok(objResponse);
        }



        [Route("[action]")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] Proveedor s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetProveedores, cadenaConexion, s.Transaccion, xmlParam.ToString());

            Response objResponse = new Response();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["MENSAJE"].ToString();

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
