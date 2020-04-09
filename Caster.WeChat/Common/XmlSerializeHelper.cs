using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Caster.WeChat.MessageHandler.Request;

namespace Caster.WeChat.Common
{
    public class XmlSerializeHelper
    {
        /// <summary>
        /// 将Xml字符串转换成对象
        /// </summary>
        /// <param name="value">xml</param>
        /// <typeparam name="TResult">最后的实体结果</typeparam>
        /// <returns></returns>
        public static TResult StringToObject<TResult>(string value) where TResult : class
        {
            using (MemoryStream  memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                using (XmlReader xmlReader = XmlReader.Create(memoryStream))
                {
                    XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces();
                    serializerNamespaces.Add("","");
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TResult));
                    var result = xmlSerializer.Deserialize(xmlReader);
                    return result as TResult;
                }
            }
        }
        /// <summary>
        /// 将对象转换成Xml字符串
        /// </summary>
        /// <param name="source">转换的对象</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static string ObjectToXmlString<TEntity>(TEntity source) where TEntity : class
        {
            XmlSerializer serializer = new XmlSerializer(source.GetType());
            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Indent = true, Encoding = Encoding.UTF8, OmitXmlDeclaration = true
            };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memoryStream,writerSettings))
                {
                    XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces();
                    serializerNamespaces.Add("","");
                    serializer.Serialize(writer,source,serializerNamespaces);
                    return Encoding.UTF8.GetString(memoryStream.GetBuffer());
                }
            }
        }

        public static string XmlToString(XmlDocument xml)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented; xml.Save(writer);
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }
    }
}