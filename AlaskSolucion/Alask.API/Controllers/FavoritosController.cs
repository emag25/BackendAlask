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

    public class FavoritosController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Favorito>> Get([FromBody] Favorito p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.GetFavoritos, cadenaConexion, p.Transaccion, xmlParam.ToString());
            List<Favorito> listData = new List<Favorito>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Favorito objResponse = new Favorito {                      
                            Id = Convert.ToInt32(row["id_favoritos"]),
                            Usuario = new Usuario()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                Nombre = row["nombre_usuario"].ToString(),
                                Apellido = row["apellido_usuario"].ToString(),
                                Email = row["email_usuario"].ToString(),
                                Telefono = row["telefono_usuario"].ToString(),
                                Direccion = row["direccion_usuario"].ToString(),
                                Provincia = new Provincia()
                                {
                                    Id = Convert.ToInt32(row["id_provincia"]),
                                    Nombre = row["nombre_provincia"].ToString()
                                },
                                Rol = row["rol_usuario"].ToString(),                                
                            },
                            Producto = new Producto()
                            {
                                Id = Convert.ToInt32(row["id_producto"]),
                                Nombre = row["nombre_producto"].ToString(),
                                Descripcion = row["descripcion_producto"].ToString(),
                                Imagen = row["imagen_producto"].ToString(),
                                Precio = float.Parse(row["precio_producto"].ToString()),
                                Stock = int.Parse(row["stock_producto"].ToString()),
                                Categoria = new Categoria()
                                {
                                    Id = Convert.ToInt32(row["id_categoria"]),
                                    Nombre = row["nombre_categoria"].ToString(),
                                    FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_categoria"].ToString())),
                                    Estado = row["estado_categoria"].ToString()
                                },
                                Proveedor = new Proveedor()
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
                                },
                                FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_producto"].ToString())),
                                Estado = row["estado_producto"].ToString()
                            },
                            FechaIngreso = DateOnly.FromDateTime(DateTime.Parse(row["fechaingreso_favorito"].ToString())),
                            Estado = row["estado_producto_favorito"].ToString()
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
        public async Task<ActionResult<Response>> Set([FromBody] Favorito p)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(p);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(SPNames.SetFavoritos, cadenaConexion, p.Transaccion, xmlParam.ToString());
            Response objResponse = new Response();
            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
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