using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlC1ient;
using System.Xml;

namespace Alask.API.CodeGeneral
{
    public static class DBXmlMethods
    {
        public static XDocument GetXml<T>(T criterio)
        {
            XDocument resultado = new XDocument(new XDeclaration("1.0", "utf-8", "true"));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using XmlWriter xw = resultado.CreateWriter(); xs.Serialize(xw, criterio);
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }






    }
}