namespace Artillery.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public static class XmlSerialization
    {

        public static T[] DeserializeXml<T>(string xmlString, string rootElement)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElement));
            using var stringReader = new StringReader(xmlString);

            return (T[])xmlSerializer.Deserialize(stringReader);
        }

    }
}
