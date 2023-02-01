﻿using Alask.API.CodeGeneral;
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
        public async Task<ActionResult<Provincia>> GetAll([FromBody] Provincia provincia)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(provincia);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProvincias", cadenaConexion, "consultar_todo", xmlParam.ToString());
            List<Provincia> listData = new List<Provincia>();

            return Ok(listData);
        }



        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Provincia>> GetById([FromBody] Provincia provincia)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(provincia);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProvincias", cadenaConexion, "consultar_porId", xmlParam.ToString());
            List<Provincia> listData = new List<Provincia>();

            return Ok(listData);
        }



        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Provincia>> GetByName([FromBody] Provincia provincia)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlParam = DBXmlMethods.GetXml(provincia);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase("GetProvincias", cadenaConexion, "consultar_porNombre", xmlParam.ToString());
            List<Provincia> listData = new List<Provincia>();

            return Ok(listData);
        }


    }

}