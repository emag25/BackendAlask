using Alask.API.CodeGeneral;
using Alask.BL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;


namespace Alask.API {

    [ApiController]
    [Route("api/[controller]")]

    public class SolicitudesController : Controller
    {


        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Solicitud>> Get([FromBody] Solicitud s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetSolicitudes, cadenaConexion, s.Transaccion, xmlParam.ToString());
            List<Solicitud> listData = new List<Solicitud>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Solicitud objResponse = new Solicitud
                        {
                            Id = Convert.ToInt32(row["id_Solicitud"]),
                            Ruc = row["ruc_Solicitud"].ToString(),
                            Nombre = row["nombre_Solicitud"].ToString(),
                            Email = row["email_Solicitud"].ToString(),
                            Telefono = row["telefono_Solicitud"].ToString(),
                            Provincia = new Provincia()
                            {
                                Id = Convert.ToInt32(row["id_provincia"]),
                                Nombre = row["nombre_provincia"].ToString()
                            },
                            FechaEnvio = DateOnly.FromDateTime(DateTime.Parse(row["fechaEnvio_Solicitud"].ToString())),
                            Estado = row["estado_Solicitud"].ToString()
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




        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Response>> Set([FromBody] Solicitud s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetSolicitudes, cadenaConexion, s.Transaccion, xmlParam.ToString());

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
        public async Task<ActionResult> Update([FromBody] Solicitud s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetSolicitudes, cadenaConexion, s.Transaccion, xmlParam.ToString());

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
        public async Task<ActionResult> Delete([FromBody] Solicitud s)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(s);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetSolicitudes, cadenaConexion, s.Transaccion, xmlParam.ToString());

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